// See https://aka.ms/new-console-template for more information

// Example connection strings:
//    activemq:tcp://activemqhost:61616
//    stomp:tcp://activemqhost:61613
//    ems:tcp://tibcohost:7222
//    msmq://localhost


using Apache.NMS;
using Apache.NMS.Util;

var connecturi = new Uri("ems:tcp://tibcohost1:7222,tcp://tibcohost2:7222");

Console.WriteLine("About to connect to " + connecturi);
var factory = new NMSConnectionFactory(connecturi);

var username = "admin";
var password = "";

using var connection = factory.CreateConnection(username, password);
using var session = connection.CreateSession();
//
// Embedded destination type in the name:
//    IDestination destination = SessionUtil.GetDestination(session, "queue://FOO.BAR");
//    Debug.Assert(destination is IQueue);
//    IDestination destination = SessionUtil.GetDestination(session, "topic://FOO.BAR");
//    Debug.Assert(destination is ITopic);
//
// Defaults to queue if type is not specified:
//    IDestination destination = SessionUtil.GetDestination(session, "FOO.BAR");
//    Debug.Assert(destination is IQueue);
//
    
var destination = SessionUtil.GetDestination(session, "queue://abc");

Console.WriteLine("Using destination: " + destination);
    
// Create a consumer and producer
using(IMessageConsumer consumer = session.CreateConsumer(destination))
    using(IMessageProducer producer = session.CreateProducer(destination))
    {
        var receiveTimeout = TimeSpan.FromSeconds(10);
        
        // Start the connection so that messages will be processed.
        connection.Start();
        producer.DeliveryMode = MsgDeliveryMode.Persistent;
        producer.RequestTimeout = receiveTimeout;

        consumer.Listener += new MessageListener(OnMessage);

        // Send a message
        ITextMessage request = session.CreateTextMessage("Hello World!");
        request.NMSCorrelationID = "abc";
        request.Properties["NMSXGroupID"] = "cheese";
        request.Properties["myHeader"] = "Cheddar";

        producer.Send(request);

        Console.ReadLine();
    }

static void OnMessage(IMessage receivedMsg)
{
    var message = receivedMsg as ITextMessage;
    Console.WriteLine("Received message with ID:   " + message?.NMSMessageId);
    Console.WriteLine("Received message with text: " + message?.Text);
}