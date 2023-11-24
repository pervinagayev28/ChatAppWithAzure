using System;
using System.Collections.Generic;

namespace MiniChatingApp;

public partial class Message
{
    public int Id { get; set; }

    public int RightOrLeft { get; set; }

    public string Message1 { get; set; } = null!;
}
