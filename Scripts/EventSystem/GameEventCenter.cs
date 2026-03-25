using System;
using System.Collections.Generic;

namespace EventSystem
{
    public enum GameEvent
    {
        SkillCast,
        SkillHit,
        CharacterDead
    }

    public static class GameEventCenter
    {
        private static readonly Dictionary<GameEvent, Action<object>> eventTable = new();

        public static void Subscribe(GameEvent eventType, Action<object> handler)
        {
            if (!eventTable.ContainsKey(eventType))
            {
                eventTable[eventType] = null;
            }

            eventTable[eventType] += handler;
        }

        public static void Unsubscribe(GameEvent eventType, Action<object> handler)
        {
            if (eventTable.ContainsKey(eventType))
            {
                eventTable[eventType] -= handler;
            }
        }

        public static void Publish(GameEvent eventType, object data = null)
        {
            if (eventTable.ContainsKey(eventType))
            {
                eventTable[eventType]?.Invoke(data);
            }
        }
    }
}