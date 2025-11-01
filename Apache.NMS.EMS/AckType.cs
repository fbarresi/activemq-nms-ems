namespace Apache.NMS.EMS
{
    public enum AckType
    {
        ACCEPTED = 0,
        REJECTED = 1,
        RELEASED = 2,
        MODIFIED_FAILED = 3,
        MODIFIED_FAILED_UNDELIVERABLE = 4,
        // Conceptual
        DELIVERED = 5
    }
}