using System;

namespace Mirage.Collections
{
    public interface INotifyCollectionReset
    {
        event EventHandler<EventArgs> Reset;
    }
}
