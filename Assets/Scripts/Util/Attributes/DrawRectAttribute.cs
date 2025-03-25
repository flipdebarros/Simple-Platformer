using System;

[System.AttributeUsage(
	System.AttributeTargets.Field | AttributeTargets.Property,
	AllowMultiple = true
)]
public class DrawRectAttribute : Attribute
{
}