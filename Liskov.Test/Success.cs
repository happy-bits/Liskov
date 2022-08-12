using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Liskov.Test
{
    [TestClass]
    public class Success
    {
        class Rectangle
        {
            private readonly double _width;
            private readonly double _height;

            public double Area => _width * _height;

            public double Width => _width;  
            public double Height => _height;

            public Rectangle(double width, double height)
            {
                _width = width;
                _height = height;
            }

            public double X { get; set; }
            public double Y { get; set; }
        }

        class Square : Rectangle
        {
            public Square(double size) : base(size, size)
            {
            }
        }

        /* Not possible to write this function anymore
        static Rectangle AdjustSize(Rectangle rectangle)
        {
            rectangle.SetWidth(3);
            rectangle.SetHeight(4);
            return rectangle;
        }*/

        static Rectangle AdjustPosition(Rectangle rectangle)
        {
            rectangle.X = 100;
            rectangle.Y = 200;
            return rectangle;
        }

        [TestMethod]
        public void HappyPath()
        {
            var rectangle = new Rectangle(3, 4);
            rectangle.Width.ShouldBe(3);
            rectangle.Height.ShouldBe(4);

            AdjustPosition(rectangle);

            rectangle.Width.ShouldBe(3);
            rectangle.Height.ShouldBe(4);

            rectangle.X.ShouldBe(100);
            rectangle.Y.ShouldBe(200);
        }


        [TestMethod]
        public void HappyPath2()
        {
            var square = new Square(2);
            square.Width.ShouldBe(2);
            square.Height.ShouldBe(2);

            AdjustPosition(square);

            square.Width.ShouldBe(2);
            square.Height.ShouldBe(2);

            square.X.ShouldBe(100);
            square.Y.ShouldBe(200);
        }

    }
}