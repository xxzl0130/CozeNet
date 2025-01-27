using CommandLine;


Parser.Default.ParseArguments<Test.Options>(args).WithParsed<Test.Options>(o =>
{
    Test.Test.Run(o);
});