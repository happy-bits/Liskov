using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Liskov.Test
{
    [TestClass]
    public class Fail1
    {
        class Rectangle
        {
            private double _width;
            private double _height;

            public double Area => _width * _height;

            public double Width => _width;  
            public double Height => _height;

            public Rectangle(double width, double height)
            {
                _width = width;
                _height = height;
            }

            public virtual void SetWidth(double width)
            {
                _width = width;
            }

            public virtual void SetHeight(double height)
            {
                _height = height;
            }
        }

        class Square : Rectangle
        {
            public Square(double size) : base(size, size)
            {
            }
            public override void SetHeight(double height)
            {
                base.SetHeight(height);
                base.SetWidth(height);
            }
            public override void SetWidth(double width)
            {
                base.SetHeight(width);
                base.SetWidth(width);
            }
        }

        static void AdjustSize(Rectangle rectangle)
        {
            rectangle.SetWidth(3);
            rectangle.SetHeight(4);
        }


        [TestMethod]
        public void HappyPath1()
        {
            var square = new Square(2);
            square.Width.ShouldBe(2);
            square.Height.ShouldBe(2);

            square.SetWidth(5);
            square.Width.ShouldBe(5);
            square.Height.ShouldBe(5);
        }

        [TestMethod]
        public void HappyPath2()
        {
            var rectangle = new Rectangle(2, 2);
            AdjustSize(rectangle);
            rectangle.Area.ShouldBe(12);
        }

        [TestMethod]
        public void WillFail()
        {
            var square = new Square(2);
            AdjustSize(square);
            square.Area.ShouldBe(12);  // Fail: will be 16
        }
    }
}