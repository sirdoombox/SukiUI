using System;
using Avalonia.Controls.Notifications;

namespace SukiUI.Toasts
{
    // TODO: Docs
    public static class FluentSukiToastBuilder
    {
        #region Basics

        public static SukiToastBuilder CreateToast(this ISukiToastManager manager) => new(manager);

        /// <summary>
        /// Creates a simple informational toast that can be dismissed by clicking and is otherwise dismissed after 3 seconds.
        /// This doesn't set the title/content and you should use <see cref="WithTitle"/> and <see cref="WithContent"/>
        /// </summary>
        public static SukiToastBuilder CreateSimpleInfoToast(this ISukiToastManager manager)
        {
            return new SukiToastBuilder(manager)
                .Dismiss().After(TimeSpan.FromSeconds(3))
                .Dismiss().ByClicking();
        }

        public static SukiToastBuilder WithTitle(this SukiToastBuilder builder, string title)
        {
            builder.SetTitle(title);
            return builder;
        }

        public static SukiToastBuilder WithContent(this SukiToastBuilder builder, object? content)
        {
            builder.SetContent(content);
            return builder;
        }

        public static SukiToastBuilder OfType(this SukiToastBuilder builder, NotificationType type)
        {
            builder.SetType(type);
            return builder;
        }

        #endregion

        #region Dismissing

        /// <summary>
        /// Begins a dismiss statement for the toast - Follow this with something like <see cref="After"/>.
        /// </summary>
        public static SukiToastBuilder.DismissToast Dismiss(this SukiToastBuilder builder) => new(builder);

        /// <summary>
        /// Automatically dismisses the toast after the given amount of time.
        /// </summary>
        public static SukiToastBuilder After(this SukiToastBuilder.DismissToast dismiss, TimeSpan after)
        {
            dismiss.Builder.Delay(after, toast => dismiss.Builder.Manager.Dismiss(toast));
            return dismiss.Builder;
        }

        /// <summary>
        /// Allows the toast to be dismissed by clicking anywhere on the toast.
        /// </summary>
        public static SukiToastBuilder ByClicking(this SukiToastBuilder.DismissToast dismiss)
        {
            dismiss.Builder.SetCanDismissByClicking(true);
            return dismiss.Builder;
        }

        #endregion

        #region Actions

        /// <summary>
        /// The action provided will be called if the body of the toast is clicked.
        /// </summary>
        public static SukiToastBuilder OnClicked(this SukiToastBuilder builder, Action<ISukiToast> action)
        {
            builder.SetOnClicked(action);
            return builder;
        }
        
        /// <summary>
        /// The action provided will be called when the toast is dismissed for any reason, including clicking.
        /// </summary>
        public static SukiToastBuilder OnDismissed(this SukiToastBuilder builder, Action<ISukiToast> onDismissAction)
        {
            builder.SetOnDismiss(onDismissAction);
            return builder;
        }

        public static SukiToastBuilder WithActionButton(this SukiToastBuilder builder, object buttonContent, Action<ISukiToast> onClicked, bool dismissOnClick =  false)
        {
            builder.AddActionButton(buttonContent, onClicked, dismissOnClick);
            return builder;
        }

        #endregion
    }
}