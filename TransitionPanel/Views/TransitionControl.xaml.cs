using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

// WPF で画面遷移する方法 4 YKSoftware
// http://yujiro15.net/blog/index.php?id=149

namespace TransitionPanel.Views
{
    /// <summary>
    /// TransitionPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class TransitionControl : UserControl
    {
        /// <summary>
        /// 時刻ゼロ
        /// </summary>
        private static readonly TimeSpan TimeZero = TimeSpan.FromMilliseconds(0);

        /// <summary>
        /// アニメーション時間
        /// </summary>
        private static readonly TimeSpan AnimationTime = TimeSpan.FromMilliseconds(500);

        /// <summary>
        /// 水平方向の遷移量
        /// </summary>
        private double HorizontalOffset { get => this.ActualWidth + 10; }

        public TransitionControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        /// <summary>
        /// アニメーションの方向を表します。
        /// </summary>
        public enum TransitDirections
        {
            ToLeft, ToRight
        }

        /// <summary>
        /// 遷移状態を表します。
        /// </summary>
        public enum TransitionStates
        {
            DisplayA, DisplayB
        }

        #region Content 依存関係プロパティ

        /// <summary>
        /// Content 依存関係プロパティを定義し直します。
        /// </summary>
        public static readonly new DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(object),
                typeof(TransitionControl),
                new UIPropertyMetadata(null, OnConentPropertyChanged));

        /// <summary>
        /// コンテンツを取得または設定します。
        /// </summary>
        public new object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        #endregion Content 依存関係プロパティ

        #region ContentA 依存関係プロパティ

        /// <summary>
        /// ContentA 依存関係プロパティのキーを定義します。
        /// </summary>
        private static readonly DependencyPropertyKey ContentAPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(ContentA),
                typeof(object),
                typeof(TransitionControl),
                new UIPropertyMetadata(null));

        /// <summary>
        /// ContentA 依存関係プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty ContentAProperty =
            ContentAPropertyKey.DependencyProperty;

        /// <summary>
        /// コンテンツのためのバッファ A を取得します。
        /// </summary>
        public object ContentA
        {
            get { return GetValue(ContentAProperty); }
            private set { SetValue(ContentAPropertyKey, value); }
        }

        #endregion ContentA 依存関係プロパティ

        #region ContentB 依存関係プロパティ

        /// <summary>
        /// ContentB 依存関係プロパティのキーを定義します。
        /// </summary>
        private static readonly DependencyPropertyKey ContentBPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(ContentB),
                typeof(object),
                typeof(TransitionControl),
                new UIPropertyMetadata(null));

        /// <summary>
        /// ContentB 依存関係プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty ContentBProperty =
            ContentBPropertyKey.DependencyProperty;

        /// <summary>
        /// コンテンツのためのバッファ B を取得します。
        /// </summary>
        public object ContentB
        {
            get { return GetValue(ContentBProperty); }
            private set { SetValue(ContentBPropertyKey, value); }
        }

        #endregion ContentB 依存関係プロパティ

        #region State 依存関係プロパティ

        /// <summary>
        /// State 依存関係プロパティのキーを定義します。
        /// </summary>
        private static readonly DependencyPropertyKey StatePropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(State),
                typeof(TransitionStates),
                typeof(TransitionControl),
                new UIPropertyMetadata(TransitionStates.DisplayB));

        /// <summary>
        /// State 依存関係プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty StateProperty =
            StatePropertyKey.DependencyProperty;

        /// <summary>
        /// 遷移状態を取得します。
        /// </summary>
        public TransitionStates State
        {
            get { return (TransitionStates)GetValue(StateProperty); }
            private set { SetValue(StatePropertyKey, value); }
        }

        #endregion State 依存関係プロパティ

        #region TransitDirection 依存関係プロパティ

        /// <summary>
        /// TransitDirection 依存関係プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty TransitDirectionProperty =
            DependencyProperty.Register(
                nameof(TransitDirection),
                typeof(TransitDirections),
                typeof(TransitionControl),
                new UIPropertyMetadata(TransitDirections.ToLeft));

        /// <summary>
        /// 画面遷移方向を取得または設定します。
        /// </summary>
        public TransitDirections TransitDirection
        {
            get { return (TransitDirections)GetValue(TransitDirectionProperty); }
            set { SetValue(TransitDirectionProperty, value); }
        }

        #endregion TransitDirection 依存関係プロパティ

        #region OffsetXA 依存関係プロパティ

        /// <summary>
        /// OffsetXA 依存関係プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty OffsetXAProperty =
            DependencyProperty.Register(
                nameof(OffsetXA),
                typeof(double),
                typeof(TransitionControl),
                new UIPropertyMetadata(default(double)));

        /// <summary>
        /// コンテンツのためのバッファ A の水平方向オフセットを取得または設定します。
        /// </summary>
        public double OffsetXA
        {
            get { return (double)GetValue(OffsetXAProperty); }
            set { SetValue(OffsetXAProperty, value); }
        }

        #endregion OffsetXA 依存関係プロパティ

        #region OffsetYA 依存関係プロパティ

        /// <summary>
        /// OffsetYA 依存関係プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty OffsetYAProperty =
            DependencyProperty.Register(
                nameof(OffsetYA),
                typeof(double),
                typeof(TransitionControl),
                new UIPropertyMetadata(default(double)));

        /// <summary>
        /// コンテンツのためのバッファ A の垂直方向オフセットを取得または設定します。
        /// </summary>
        public double OffsetYA
        {
            get { return (double)GetValue(OffsetYAProperty); }
            set { SetValue(OffsetYAProperty, value); }
        }

        #endregion OffsetYA 依存関係プロパティ

        #region OffsetXB 依存関係プロパティ

        /// <summary>
        /// OffsetXB 依存関係プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty OffsetXBProperty =
            DependencyProperty.Register(
                nameof(OffsetXB),
                typeof(double),
                typeof(TransitionControl),
                new UIPropertyMetadata(default(double)));

        /// <summary>
        /// コンテンツのためのバッファ B の水平方向オフセットを取得または設定します。
        /// </summary>
        public double OffsetXB
        {
            get { return (double)GetValue(OffsetXBProperty); }
            set { SetValue(OffsetXBProperty, value); }
        }

        #endregion OffsetXB 依存関係プロパティ

        #region OffsetYB 依存関係プロパティ

        /// <summary>
        /// OffsetYB 依存関係プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty OffsetYBProperty =
            DependencyProperty.Register(
                nameof(OffsetYB),
                typeof(double),
                typeof(TransitionControl),
                new UIPropertyMetadata(default(double)));

        /// <summary>
        /// コンテンツのためのバッファ B の垂直方向オフセットを取得または設定します。
        /// </summary>
        public double OffsetYB
        {
            get { return (double)GetValue(OffsetYBProperty); }
            set { SetValue(OffsetYBProperty, value); }
        }

        #endregion OffsetYB 依存関係プロパティ

        #region イベントハンドラ

        /// <summary>
        /// Load イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var storyboard = new Storyboard
            {
                Children = new TimelineCollection()
                {
                    CreateMoveAnimation(TimeZero, TimeZero, this.HorizontalOffset, nameof(OffsetXB)),
                }
            };
            storyboard.Begin();
        }

        /// <summary>
        /// Content 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnConentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TransitionControl;
            if (control.IsInitialized) control.SwapDisplay();
        }

        #endregion イベントハンドラ

        #region ヘルパ

        /// <summary>
        /// コンテンツを入れ替えます。
        /// </summary>
        private void SwapDisplay()
        {
            if (this.State == TransitionStates.DisplayA)
            {
                this.ContentB = this.Content;
                this.State = TransitionStates.DisplayB;
            }
            else
            {
                this.ContentA = this.Content;
                this.State = TransitionStates.DisplayA;
            }

            if ((this.ContentA != null) && (this.ContentB != null))
                StartAnimation();
        }

        /// <summary>
        /// 画面遷移を開始します。
        /// </summary>
        private void StartAnimation()
        {
            var storyboard =
                this.State == TransitionStates.DisplayA ?
                CreateAnimation1to2(this.TransitDirection, nameof(OffsetXB), nameof(OffsetXA)) :
                CreateAnimation1to2(this.TransitDirection, nameof(OffsetXA), nameof(OffsetXB));
            storyboard.Begin();
        }

        /// <summary>
        /// Content1 から Content2 へ遷移するためのストーリーボードを生成します。
        /// </summary>
        /// <param name="direction">遷移する方向を指定します。</param>
        /// <returns>生成したストーリーボードを返します。</returns>
        private Storyboard CreateAnimation1to2(TransitDirections direction, string offsetX1, string offsetX2)
        {
            var horizontalOffset = this.HorizontalOffset;
            var storyboard = new Storyboard
            {
                Children = direction == TransitDirections.ToLeft ?
                new TimelineCollection()
                {
                    CreateMoveAnimation(TimeZero, TimeZero, horizontalOffset, offsetX2),
                    CreateMoveAnimation(TimeZero, AnimationTime, 0, offsetX2),
                    CreateMoveAnimation(TimeZero, AnimationTime, -horizontalOffset, offsetX1),
                } :
                new TimelineCollection()
                {
                    CreateMoveAnimation(TimeZero, TimeZero, -horizontalOffset, offsetX2),
                    CreateMoveAnimation(TimeZero, AnimationTime, 0, offsetX2),
                    CreateMoveAnimation(TimeZero, AnimationTime, horizontalOffset, offsetX1),
                }
            };
            return storyboard;
        }
        
        /// <summary>
        /// Double 型のプロパティに対するアニメーションを生成します。
        /// </summary>
        /// <param name="beginTime">アニメーションの開始時間を指定します。</param>
        /// <param name="duration">アニメーションの実行時間を指定します。</param>
        /// <param name="to">プロパティ値の最終値を指定します。</param>
        /// <param name="targetPropertyName">対象とするプロパティ名を指定します。</param>
        /// <returns>Storyboard の添付プロパティを設定したアニメーションを返します。</returns>
        private DoubleAnimation CreateMoveAnimation(TimeSpan beginTime, TimeSpan duration, double to, string targetPropertyName)
        {
            var animation = new DoubleAnimation()
            {
                To = to,
                BeginTime = beginTime,
                Duration = new Duration(duration),
                AccelerationRatio = 0.3,
                DecelerationRatio = 0.3,
            };
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath(targetPropertyName));

            return animation;
        }

        #endregion ヘルパ

    }
}