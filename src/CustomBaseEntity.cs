using System;

namespace Microsoft.EntityFrameworkCore;

public class CustomBaseEntity
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
