using Apache.NMS.Policies;

namespace Apache.NMS.EMS
{
    public class DefaultRedeliveryPolicy : RedeliveryPolicy
    {
        public override int GetOutcome(IDestination destination)
        {
            return (int) AckType.MODIFIED_FAILED_UNDELIVERABLE;
        }
    }
}