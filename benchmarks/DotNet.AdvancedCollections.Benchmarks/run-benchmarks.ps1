# Script para ejecutar benchmarks de grafos
# Uso: .\run-benchmarks.ps1 [benchmark-name]

param(
    [string]$BenchmarkFilter = "*GraphBenchmarks*"
)

Write-Host "================================" -ForegroundColor Cyan
Write-Host "DotNet.AdvancedCollections Benchmarks" -ForegroundColor Cyan
Write-Host "================================" -ForegroundColor Cyan
Write-Host ""

$projectPath = Split-Path -Parent $MyInvocation.MyCommand.Path

Write-Host "Navegando al directorio del proyecto..." -ForegroundColor Yellow
Set-Location $projectPath

Write-Host "Compilando proyecto en modo Release..." -ForegroundColor Yellow
dotnet build -c Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al compilar el proyecto" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Ejecutando benchmarks: $BenchmarkFilter" -ForegroundColor Green
Write-Host ""

dotnet run -c Release --filter $BenchmarkFilter

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "================================" -ForegroundColor Cyan
    Write-Host "Benchmarks completados!" -ForegroundColor Green
    Write-Host "Resultados en: .\BenchmarkDotNet.Artifacts\results\" -ForegroundColor Yellow
    Write-Host "================================" -ForegroundColor Cyan
} else {
    Write-Host ""
    Write-Host "Error al ejecutar benchmarks" -ForegroundColor Red
}
