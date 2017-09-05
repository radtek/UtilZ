﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UtilZ.Lib.WPF.WaitingControls
{
    /// <summary>
    /// MetroRotaionIndicator.xaml 的交互逻辑
    /// </summary>
    public partial class MetroRotaionIndicator : UserControl
    {
        #region 依赖属性
        /// <summary>
        /// 动画圆点半径依赖属性
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.RegisterAttached("Radius", typeof(double), typeof(MetroRotaionIndicator), new PropertyMetadata(3.0d, PropertyChanged));

        /// <summary>
        /// 圆点颜色依赖属性
        /// </summary>
        public static readonly DependencyProperty EllipseColorProperty = DependencyProperty.RegisterAttached("EllipseColor", typeof(Brush), typeof(MetroRotaionIndicator), new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Colors.Blue), PropertyChanged));

        /// <summary>
        /// 动画背景色依赖属性
        /// </summary>
        public static readonly DependencyProperty AnimalBackgroundProperty = DependencyProperty.RegisterAttached("AnimalBackground", typeof(Brush), typeof(MetroRotaionIndicator), new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Color.FromRgb(240, 240, 240)), PropertyChanged));

        /// <summary>
        /// 获取或设置动画圆点半径
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("获取或设置动画圆点半径")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public double Radius
        {
            get
            {
                return (double)this.GetValue(MetroRotaionIndicator.RadiusProperty);
            }
            set
            {
                if (Math.Abs(this.Radius - value) < 0.0001)
                {
                    return;
                }

                this.SetValue(MetroRotaionIndicator.RadiusProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置圆点颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("获取或设置圆点颜色依赖属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color EllipseColor
        {
            get
            {
                return ((SolidColorBrush)this.GetValue(MetroRotaionIndicator.EllipseColorProperty)).Color;
            }
            set
            {
                if (this.EllipseColor.Equals(value))
                {
                    return;
                }

                this.SetValue(MetroRotaionIndicator.EllipseColorProperty, new SolidColorBrush(value));
            }
        }

        /// <summary>
        /// 获取或设置圆点颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("获取或设置圆点填充Brush")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Brush EllipseColorBrush
        {
            get
            {
                return (Brush)this.GetValue(MetroRotaionIndicator.EllipseColorProperty);
            }
            set
            {
                if (this.EllipseColorBrush.Equals(value))
                {
                    return;
                }

                this.SetValue(MetroRotaionIndicator.EllipseColorProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置动画背景色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("获取或设置动画背景色")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color AnimalBackground
        {
            get
            {
                return ((SolidColorBrush)this.GetValue(MetroRotaionIndicator.AnimalBackgroundProperty)).Color;
            }
            set
            {
                if (this.AnimalBackground.Equals(value))
                {
                    return;
                }

                this.SetValue(MetroRotaionIndicator.AnimalBackgroundProperty, new SolidColorBrush(value));
            }
        }

        /// <summary>
        /// 获取或设置动画背景色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("获取或设置动画背景填充Brush")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Brush AnimalBackgroundBrush
        {
            get
            {
                return (Brush)this.GetValue(MetroRotaionIndicator.AnimalBackgroundProperty);
            }
            set
            {
                if (this.AnimalBackgroundBrush.Equals(value))
                {
                    return;
                }

                this.SetValue(MetroRotaionIndicator.AnimalBackgroundProperty, value);
            }
        }

        /// <summary>
        /// 动画背景色依赖属性值改变事件
        /// </summary>
        /// <param name="d">依赖父控件</param>
        /// <param name="e">属性参数</param>
        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MetroRotaionIndicator control = d as MetroRotaionIndicator;
            if (e.Property == AnimalBackgroundProperty)
            {
                control.canvasAnimal.Background = e.NewValue as SolidColorBrush;
            }
            else if (e.Property == EllipseColorProperty)
            {
                SolidColorBrush ellipseFillBrush = e.NewValue as SolidColorBrush;
                Ellipse ellipse = null;
                foreach (var el in control.canvasAnimal.Children)
                {
                    ellipse = el as Ellipse;
                    if (ellipse != null)
                    {
                        ellipse.Fill = ellipseFillBrush;
                    }
                }
            }
            else if (e.Property == RadiusProperty)
            {
                double radius = (double)e.NewValue;
                Ellipse ellipse = null;
                foreach (var el in control.canvasAnimal.Children)
                {
                    ellipse = el as Ellipse;
                    if (ellipse != null)
                    {
                        ellipse.Width = radius;
                        ellipse.Height = radius;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 旋转动画
        /// </summary>
        private Storyboard _trans = null;

        private Style _ellipseStyle = null;

        private System.Windows.Media.RotateTransform _rotateTransform = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MetroRotaionIndicator()
        {
            InitializeComponent();

            this._trans = this.Resources["Trans"] as Storyboard;
            this._ellipseStyle = canvasAnimal.Resources["EllipseStyle"] as Style;
            this._rotateTransform = ((System.Windows.Media.RotateTransform)((((System.Windows.Media.TransformGroup)(((System.Windows.Setter)this._ellipseStyle.Setters[3])).Value)).Children[2]));

            this.SizeChanged += MetroRotaionIndicator_SizeChanged;

            //this.Loaded += ((sender, e) =>
            //{
            //    this.StartAnimal();
            //});
        }

        void MetroRotaionIndicator_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double radius = e.NewSize.Height < e.NewSize.Width ? e.NewSize.Height : e.NewSize.Width;
            //this._rotateTransform .CenterX=
            //this._ellipseStyle.Setters.Select((s) => {s.pr });
        }

        /// <summary>
        /// 启动动画
        /// </summary>
        public void StartAnimal()
        {
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                this.Excute(el1);
                Thread.Sleep(140);
                this.Excute(el2);
                Thread.Sleep(170);
                this.Excute(el3);
                Thread.Sleep(170);
                this.Excute(el4);
                Thread.Sleep(170);
                this.Excute(el5);
                Thread.Sleep(170);
                this.Excute(el6);
            });
        }

        /// <summary>
        /// 启动一个动画
        /// </summary>
        /// <param name="el"></param>
        private void Excute(Ellipse el)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                el.BeginStoryboard(this._trans);
            }));
        }

        /// <summary>
        /// 停止动画
        /// </summary>
        public void StopAnimal()
        {
            this._trans.Stop(el1);
            this._trans.Stop(el2);
            this._trans.Stop(el3);
            this._trans.Stop(el4);
            this._trans.Stop(el5);
            this._trans.Stop(el6);
        }
    }
}
