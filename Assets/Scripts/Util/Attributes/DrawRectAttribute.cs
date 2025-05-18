using System;

[System.AttributeUsage(
	System.AttributeTargets.Field,
	AllowMultiple = true
)]
public class DrawRectAttribute : Attribute
{
}