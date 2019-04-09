using System;
using Xunit;
using AppliFraisGSB;

namespace TestAppliFrais
{
    public class UnitTest1
    {
        [Fact]
        public void RenvoilaDateFormateeSQL()
        {
            VisiteWindow visiteWindow = new VisiteWindow();

            var result = visiteWindow.Format("04/04/2019");

            Assert.Equal(result, "2019-04-04");


        }
    }
}
