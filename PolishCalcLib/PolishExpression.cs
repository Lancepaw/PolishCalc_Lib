namespace PolishCalcLib {
public class PolishExpression {
	private double leftSide  { get; set; }
	private double rightSide { get; set; }
	private PolishExpression? leftExpression  { get; set; }
	private PolishExpression? rightExpression { get; set; }
	public char? action    { get; set; }

	public PolishExpression() 
	{	action    = null;
		leftSide  = 0 ; 
		rightSide = 0 ;
	}

	public PolishExpression( double l, double r, char a )
	{	action    = a;
		leftSide  = l;
		rightSide = r;
	}

	public PolishExpression( PolishExpression l, PolishExpression r, char a)
	{	action          = a;
		leftExpression  = l;
		rightExpression = r;
	}

	public PolishExpression( double l, PolishExpression r, char a )
	{	action          = a;
		leftSide        = l;
		rightExpression = r;
	}

	public PolishExpression( PolishExpression l, double r, char a)
	{	action          = a;
		rightSide       = r;
		leftExpression  = l;
	}

	public PolishExpression( object l, object r, char a)
	{	if( l.GetType() == typeof( PolishExpression ) )
			leftExpression = ( PolishExpression )l; else
			leftSide = ( double ) l;

		if( r.GetType() == typeof( PolishExpression ) )
			rightExpression = ( PolishExpression )r; else
			rightSide = ( double ) r;

		action = a;
	}

	public double Calculate()
	{	double res;

		res = 0;

		if( leftExpression != null )
			leftSide = leftExpression.Calculate();

		if( rightExpression != null )
			rightSide = rightExpression.Calculate();

		switch( action )
		{	case '+':
				res = leftSide + rightSide;
				break;

			case '-':
				res = leftSide - rightSide;
				break;

			case '*':
				res = leftSide * rightSide;
				break;

			case '/':
				res = leftSide / rightSide;
				break;
		}

		return res;
	}

}}
