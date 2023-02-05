using Godot;
using System;

public class EnergyBar : ProgressBar
{
	
	public float decrementScale = 2.0f;
	bool decrement = false;
	
	[Signal]
	delegate void NoEnergy();
	
	[Signal]
	delegate void QuarterEnergy();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Value = 100;
	}
	
	public override void _PhysicsProcess(float delta)
	{
		if (decrement)
		{
			ChangeProgress(delta);
		}
	}
	
	private void _on_Player_IsSprinting(bool isSprinting)
	{
		decrement = true;
		
		if (isSprinting)
		{
			decrementScale = 6.0f;
		}
		else
		{
			decrementScale = 2.0f;
		}
	}
	
	private void ChangeProgress(float delta)
	{
		Value -= decrementScale * delta;
		
		if (Value == MinValue)
		{
			EmitSignal(nameof(NoEnergy));
		}
		
		if (Value <= 25 && Value > MinValue)
		{
			EmitSignal(nameof(QuarterEnergy));
		}
	}
	
	private void _on_Player_IsRooted(float delta)
	{
		Value += delta;
	}
	
	private void _on_Player_IsStill()
	{
		decrement = false;
	}

}
