using System.Windows.Controls;
using System.Windows.Input;

public class EditableProgressBar : ProgressBar
{
    public EditableProgressBar() : base()
        => Cursor = Cursors.Hand;

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        Value = e.GetPosition(this).X * (Maximum - Minimum) / ActualWidth;
    }
}