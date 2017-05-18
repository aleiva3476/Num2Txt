# Num2Txt

Two simple classes for convert a number to spanish text.

Examples:

### C#
```
string txt1 = Alp3476.A2Num2Txt.ToString("3528.25");
Console.WriteLine(txt1);
string txt2 = Alp3476.A2Num2Txt.ToString(123456);
Console.WriteLine(txt2);
string txt3 = Alp3476.A2Num2Txt.ToString(-258241.2);
Console.WriteLine(txt3);
```

### PHP
```
include 'Num2Txt.php';

$o = new  \Alp3476\Num2Txt();
echo $o->toString(3527.25) . PHP_EOL;
// tres mil quinientos veintisiete con veinticinco

echo $o->toString(12342245281.890) . PHP_EOL;
// doce mil trescientos cuarenta y dos millones doscientos cuarenta y cinco mil doscientos ochenta y uno con ochenta y nueve

echo $o->toString(0.253) . PHP_EOL;
// cero con veinticinco
```

#### Note:

Trim decimals to 2 digits
