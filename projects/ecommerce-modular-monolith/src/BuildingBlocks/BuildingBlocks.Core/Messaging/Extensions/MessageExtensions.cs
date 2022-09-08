using System.Reflection;
using BuildingBlocks.Abstractions.Messaging;
using BuildingBlocks.Core.Extensions;

namespace BuildingBlocks.Core.Messaging.Extensions;

public static class MessageExtensions
{
    public static IEnumerable<Type> GetHandledMessageTypes(this Assembly assembly)
    {
        var messageHandlerTypes = typeof(IMessageHandler<>)
            .GetAllTypesImplementingOpenGenericInterface(new[] {assembly})
            .ToList();

        var inheritsTypes = messageHandlerTypes.SelectMany(x => x.GetInterfaces())
            .Where(x => x.GetGenericTypeDefinition() == typeof(IMessageHandler<>));

        foreach (var inheritsType in inheritsTypes)
        {
            var messageType = inheritsType.GetGenericArguments().First();
            if (messageType.IsAssignableTo(typeof(IMessage)))
            {
                yield return messageType;
            }
        }
    }
}
