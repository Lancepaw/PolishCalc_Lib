using NUnit.Framework;

using PolishCalcLib;

namespace PolishCalcLib_Test {
public class PolishCalcLibTests {
[Test]
public void IsExpressionNullTest_Empty_Error() 
{	//arrange
	bool expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "";
	expected = false;

	//act
	actual = pc.Calculate( input, out double calc, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void IsExpressionNullTest_NotEmpty_ErrorTextCheck() 
{	//arrange
	string input, notExpected, actual; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "123";
	notExpected = "Вы не ввели выражение!";

	//act
	bool res = pc.Calculate( input, out double calc, out actual );

	//assert
	Assert.AreNotEqual( notExpected, actual );
}

[Test]
public void CheckExpressionTest_ErrorTextCheck() 
{	//arrange
	string input, expected, actual; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "123";
	expected = "Выражение записано не в польской записи";

	//act
	bool res = pc.Calculate( input, out double calc, out actual );

	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_NoAction_Error() 
{	//arrange
	bool expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "123";
	expected = false;

	//act
	actual = pc.Calculate( input, out double calc, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_OnlyOneMember_Error() 
{	//arrange
	bool expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "+ 12";
	expected = false;

	//act
	actual = pc.Calculate( input, out double calc, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_BracketErrot_Error() 
{	//arrange
	bool expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "+ (- 1 2 3";
	expected = false;

	//act
	actual = pc.Calculate( input, out double calc, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_MissingAction_Error() 
{	//arrange
	bool expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "+ ( 1 2) 3";
	expected = false;

	//act
	actual = pc.Calculate( input, out double calc, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_ExcessSpaces_Error() 
{	//arrange
	bool expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "+ (- 1 2    ) 3";
	expected = false;

	//act
	actual = pc.Calculate( input, out double calc, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_SimpleSumExpression_Success() 
{	//arrange
	double expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "+ 1 2";
	expected = 3;

	//act
	bool res = pc.Calculate( input, out actual, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_SimpleSubExpression_Success() 
{	//arrange
	double expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "- 100 75";
	expected = 25;

	//act
	bool res = pc.Calculate( input, out actual, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_SimpleMultExpression_Success() 
{	//arrange
	double expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "* 2,45 3";
	expected = 2.45 * 3;

	//act
	bool res = pc.Calculate( input, out actual, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_SimpleDivExpression_Success() 
{	//arrange
	double expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "/ 7 2";
	expected = 3.5;

	//act
	bool res = pc.Calculate( input, out actual, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_DotReplacement_Success() 
{	//arrange
	double expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "* 2.45 3";
	expected = 2.45 * 3;

	//act
	bool res = pc.Calculate( input, out actual, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_CompoundExpression_Success() 
{	//arrange
	double expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "+ (* 50 3) 7";
	expected = 157;

	//act
	bool res = pc.Calculate( input, out actual, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}

[Test]
public void CheckExpressionTest_CompoundExpression2_Success() 
{	//arrange
	double expected, actual;
	string input; 
	PolishCalculator pc;

	pc = new PolishCalculator();
	input = "- (+ (* 50 3) 7) 100";
	expected = 57;

	//act
	bool res = pc.Calculate( input, out actual, out string errMsg );
			
	//assert
	Assert.AreEqual( expected, actual );
}
}}