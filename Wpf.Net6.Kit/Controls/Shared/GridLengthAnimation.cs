using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Wpf.Net6.Kit.Controls.Shared
{
    /// <summary>
    /// Animates a grid length value just like the DoubleAnimation animates a double value.
    /// </summary>
    public class GridLengthAnimation : AnimationTimeline
    {
        private bool isCompleted;

        /// <summary>
        /// Marks the animation as completed.
        /// </summary>
        public bool IsCompleted
        {
            get => isCompleted;
            set => isCompleted = value;
        }

        /// <summary>
        /// Sets the reverse value for the second animation.
        /// </summary>
        public double ReverseValue
        {
            get => (double)GetValue(ReverseValueProperty);
            set => SetValue(ReverseValueProperty, value);
        }
        /// <summary>
        /// Dependency property. Sets the reverse value for the second animation.
        /// </summary>
        public static readonly DependencyProperty ReverseValueProperty =
            DependencyProperty.Register(
                name: nameof(ReverseValue),
                propertyType: typeof(double),
                ownerType: typeof(GridLengthAnimation),
                typeMetadata: new UIPropertyMetadata(
                    defaultValue: 0.0));

        /// <summary>
        /// CLR Wrapper for the From depenendency property.
        /// </summary>
        public GridLength From
        {
            get => (GridLength)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }
        /// <summary>
        /// Dependency property for the From property
        /// </summary>
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register(
                name: nameof(From),
                propertyType: typeof(GridLength),
                ownerType: typeof(GridLengthAnimation));

        /// <summary>
        /// CLR Wrapper for the To property
        /// </summary>
        public GridLength To
        {
            get => (GridLength)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }
        /// <summary>
        /// Dependency property for the To property
        /// </summary>
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(
                name: nameof(To),
                propertyType: typeof(GridLength),
                ownerType: typeof(GridLengthAnimation));

        /// <summary>
        /// Returns the type of object to animate.
        /// </summary>
        public override Type TargetPropertyType => typeof(GridLength);

        /// <summary>
        /// Creates an instance of the animation object.
        /// </summary>
        /// <returns>Returns the instance of the GridLengthAnimation</returns>
        protected override Freezable CreateInstanceCore() => new GridLengthAnimation();

        /// <summary>
        /// Animates the grid let set.
        /// </summary>
        /// <param name="defaultOriginValue"></param>
        /// <param name="defaultDestinationValue"></param>
        /// <param name="animationClock"></param>
        /// <returns></returns>
        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            //check the animation clock event
            VerifyAnimationCompletedStatus(animationClock);

            //check if the animation was completed
            if (isCompleted)
            {
                return (GridLength)defaultDestinationValue;
            }

            //if not then create the value to animate
            double fromVal = From.Value;
            double toVal = To.Value;

            //check if the value is already collapsed
            if (((GridLength)defaultOriginValue).Value == toVal)
            {
                fromVal = toVal;
                toVal = ReverseValue;
            }
            //check to see if this is the last tick of the animation clock.
#pragma warning disable CS8629 // Nullable value type may be null.
            else if (animationClock.CurrentProgress.Value == 1.0)
#pragma warning restore CS8629 // Nullable value type may be null.
            {
                return To;
            }

            double doubleValue = fromVal > toVal
#pragma warning disable CS8629 // Nullable value type may be null.
                ? ((1.0 - animationClock.CurrentProgress.Value) * (fromVal - toVal)) + toVal
                : (animationClock.CurrentProgress.Value * (toVal - fromVal)) + fromVal;
#pragma warning restore CS8629 // Nullable value type may be null.

            return new GridLength(doubleValue, From.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
        }

        private AnimationClock? clock;

        /// <summary>
        /// registers to the completed event of the animation clock
        /// </summary>
        /// <param name=”clock”>the animation clock to notify completion status</param>
        private void VerifyAnimationCompletedStatus(AnimationClock clock)
        {
            if (this.clock == null)
            {
                this.clock = clock;
                this.clock.Completed += (_, _) => isCompleted = true;
            }
        }
    }
}
