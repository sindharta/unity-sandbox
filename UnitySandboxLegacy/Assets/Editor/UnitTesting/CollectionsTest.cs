using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
[Category("Collections")]
internal class CollectionsTest {

    [Test]
    [Category("List")]
    public void List_GetRange()
    {
        //Throws Argument Exception
        Assert.Catch<System.ArgumentException>(()=>{
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            list.GetRange(0, 20);
        });



    }

}
