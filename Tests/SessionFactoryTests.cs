using GBReaderCremaL.Infrastructure;
using GBReaderCremaL.Infrastructure.Exceptions;
using NUnit.Framework;

namespace GBReaderCremaL.Tests;

public class SessionFactoryTests
{
    private static SessionStorageFactory _fact;

    [Test]
    public void InitFactoryCorrectly()
    {
        //_fact = new SessionStorageFactory(@"\ue36\test-q210044-session.json");
        Assert.DoesNotThrow(() => new SessionStorageFactory(@"\ue36\test-q210044-session.json"));
    } 
    
    [Test]
    public void InitFactoryWithWrongFormat()
    {
        Assert.Throws<FindPathException>(() => new SessionStorageFactory(@"\ue36\test-q210044-session."));
        Assert.Throws<FindPathException>(() => new SessionStorageFactory(@"\ue36\test-q210044-session.ezgfbobg"));
        Assert.Throws<FindPathException>(() => new SessionStorageFactory(@"\ue36\test-q210044-session.a"));
        Assert.Throws<FindPathException>(() => new SessionStorageFactory(@"\ue36\test-q210044-session.nosj"));
    } 
    
    [Test]
    public void InitFactoryWithLessThan5chars()
    {
        Assert.Throws<FindPathException>(() => new SessionStorageFactory(@""));
        Assert.Throws<FindPathException>(() => new SessionStorageFactory(@"bv"));
    } 
}