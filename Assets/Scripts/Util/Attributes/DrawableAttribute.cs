using System;

[System.AttributeUsage(
	System.AttributeTargets.Class | AttributeTargets.Struct,
	AllowMultiple = true
)]
public class DrawableAttribute : Attribute
{

}