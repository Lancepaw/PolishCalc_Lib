namespace PolishCalcLib {
public class PolishCalculator {

public bool Calculate( string input, out double calcResult, out string errMsg ) 
{	bool res;

	errMsg = null;
	calcResult = 0; 
	
	res = IsExpressionNull( input, out errMsg ) && CheckExpression( input, out errMsg );

	if( res )
		calcResult = CalculatePolishExpression( input );

	return res;
}

private double CalculatePolishExpression( string input )
{	double res;
	List< PolishExpression > expressions = new List<PolishExpression> ();

	while( true )
	{	if( GetExpressionStartIndex( ref input, out int startIndex ) )
		{	expressions.Add( GetNextExpression( input, ref startIndex ) );  
			input = input.Substring( startIndex );
		}
		else
		{	break;  }
	}

	res = 0;

	foreach( PolishExpression expression in expressions )
	{	res += expression.Calculate();  }

	return res;
}

private PolishExpression GetNextExpression( string input, ref int startIndex )
{	char action;
	PolishExpression res;
	
	action = input[ startIndex ] != '(' ? input[ startIndex ] : input[ startIndex + 1 ];
	GetNextExpressionMembers( input, ref startIndex, out string leftMember, out string rightMember );

	res = new PolishExpression( ReadExpressionMember( leftMember ), ReadExpressionMember( rightMember ), action );

	return res;
}

private object ReadExpressionMember( string member )
{	object res;

	if( member[ 0 ] == '(' )
	{	int i = 0;
		res = GetNextExpression( member, ref i );
	}
	else
	{	res = double.Parse( member.Replace( '.',',' ) );  }

	return res;
}

private void GetNextExpressionMembers( string input, ref int start, out string left, out string right  )
{	int memberLength;
	string searchChar;
	
	if( input[0] == '(' )
	{	input = input.Remove( 0, 1 );
		input = input.Remove( input.Length - 1 );
	}
	input = input.Substring( start + 2 );

	memberLength = input[0] != '(' ? input.IndexOf( " " ) : FindLastBracket( input );
	left = input.Substring( 0, memberLength );
	start = memberLength + 1;

	memberLength = input.Length - start;
	right = input.Substring( start, memberLength );
	start = start + memberLength + 1;
}

private int FindLastBracket( string input )
{	int i, openCount, closeCount;

	openCount  = 0;
	closeCount = 0;

	for( i = 0; i < input.Length; i++ )
	{	if( input[i] == '(' )
			openCount++;
		else if( input[i] == ')' )
			closeCount++;

		if( openCount == closeCount )
			break;
	}

	return i + 1;
}
private bool GetExpressionStartIndex( ref string input, out int index )
{	int foundIndexes;
	bool res;
	int[] tmp;
	char[] searchSymbols;
		
	res = false;
	index = -1;
	foundIndexes = 0; 
	searchSymbols = new char[]{ '+', '-', '*', '/' };
		
	tmp = new int[ searchSymbols.Length ];

	for( int i = 0; i < searchSymbols.Length; i++ )
	{	tmp[ i ] = input.IndexOf( searchSymbols[ i ] );  
			
	if( tmp[ i ] != -1 )
		foundIndexes++;
	}

	if( foundIndexes == 0 )
		goto endLbl;

	index = input.Length - 1;

	foreach( int i in tmp )
	{	if( i != -1 && i < index)
			index = i;
	}

	res = true;

	if( index == 0 )
	{	input = input.Insert(0, " ");
		index++;
	}

	endLbl:
	return res;
}

private bool allMembersExistsCheck( string input )
{	int actions, spaces;

	spaces  = 0;
	actions = 0;

	foreach( char c in input )
	{	if( c == '+' || c == '-' || c == '*' || c == '/')
			actions++;
		else if( char.IsWhiteSpace( c ) )
			spaces++;
	}

	return actions > 0 && spaces / actions == 2;
}

private bool CheckExpression( string input, out string errMsg)
{	int  cnt, openBracket, closeBracket;
	bool res, letterFlag, bracketFlag, allMembersExistsFlag;
	
	cnt = 0;
	res = true;
	letterFlag = false;
	openBracket = 0;
	closeBracket = 0;
	
	allMembersExistsFlag = allMembersExistsCheck( input );


	foreach( char c in input )
	{	if( char.IsLetter( c ) )
		{	letterFlag = true;
			break;
		}
		
		if( c == '(')
			openBracket++;
		else if( c == ')' )
			closeBracket++;
	}

	bracketFlag = openBracket == closeBracket;

	if( letterFlag || !bracketFlag || !allMembersExistsFlag )
		goto errLbl;


	while( true )
	{	int nextIndex;

		if( !GetExpressionStartIndex( ref input, out nextIndex ) )
			break;

		if( !( ( char.IsWhiteSpace( input[ nextIndex - 1 ] ) || input[ nextIndex - 1 ] == '(' )
			&& char.IsWhiteSpace( input[ nextIndex + 1 ] ) 
			&& ( char.IsDigit( input[ nextIndex + 2 ] ) || input[ nextIndex + 2 ] == '(' )
			&& nextIndex != 0
			&& nextIndex != input.Length - 1 ) )
		{	res = false;
			break;
		}

		cnt++;
		input = input.Substring( nextIndex + 1 );
	}
	errLbl:

	if(  cnt == 0 || letterFlag || !bracketFlag || !allMembersExistsFlag )
		res = false;

	if( !res )
		errMsg = "Выражение записано не в польской записи"; else
		errMsg = "";

	return res;
}

private bool IsExpressionNull( string input, out string errMsg )
{	bool res;

	res = !string.IsNullOrEmpty( input );

	if( res )
		errMsg = ""; else
		errMsg = "Вы не ввели выражение!";

	return res;
}

}}