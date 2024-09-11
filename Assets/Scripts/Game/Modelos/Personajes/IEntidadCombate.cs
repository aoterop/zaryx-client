using System.Collections.Concurrent;
using System;
using Zaryx_Game.Estructuras;

namespace Assets.Scripts.Personajes
{
    public interface IEntidadCombate
    {
        string Nombre { get; set; }
        int Hp { get; set; }
        int Mp { get; set; }
        int MaxHp { get; set; }
        int MaxMp { get; set; }
        short Mapa { get; set; }
        short X { get; set; }
        short Y { get; set; }
        long ExperienciaQueOtorga { get; set; }
        byte Nivel { get; set; }
        byte Velocidad { get; set; }
        short AtaqueMin { get; set; }
        short AtaqueMax { get; set; }
        short Defensa { get; set; }
        short RatioCritico { get; set; }
        short AtaqueCritico { get; set; }
        bool Inmortal { get; set; } // Protege frende daño de ataque.
        bool EnShock { get; set; } // Impide atacar si está a true.
        bool AntiCriticos { get; set; }
        bool Inmovilizado { get; set; } // Impide el movimiento, pero no atacar.
        bool EnCombate { get; set; }

        ConcurrentDictionary<short, IDisposable> BuffsObservables { get; set; }
        //ConcurrentDictionary<short, Buff> Buffs { get; set; }
        //ConcurrentDictionary<short, IHabilidad> Habilidades { get; set; }
        ConcurrentDictionary<short, IDisposable> Cooldowns { get; set; }
    }
}