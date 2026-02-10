using BenchmarkDotNet.Running;

var _ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
