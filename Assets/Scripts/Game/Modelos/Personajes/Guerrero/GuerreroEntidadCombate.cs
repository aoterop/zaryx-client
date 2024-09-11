using System.Collections.Concurrent;
using System;
using Zaryx_Game.Estructuras;

namespace Assets.Scripts.Personajes
{
    public class GuerreroEntidadCombate : IEntidadCombate
    {
        #region Propiedades
        public string Nombre { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public short Mapa { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public long ExperienciaQueOtorga { get; set; }
        public byte Nivel { get; set; }
        public byte Velocidad { get; set; }

        public short AtaqueMin { get; set; }
        public short AtaqueMax { get; set; }
        public short Defensa { get; set; }
        public short RatioCritico { get; set; }
        public short AtaqueCritico { get; set; }
        public bool Inmortal { get; set; } // Protege frende daño de ataque.
        public bool EnShock { get; set; } // Impide atacar si está a true.
        public bool AntiCriticos { get; set; }
        public bool Inmovilizado { get; set; } // Impide el movimiento, pero no atacar.
        public bool EnCombate { get; set; }

        public ConcurrentDictionary<short, IDisposable> BuffsObservables { get; set; }
        //ConcurrentDictionary<short, Buff> Buffs { get; set; }
        //ConcurrentDictionary<short, IHabilidad> Habilidades { get; set; }
        public ConcurrentDictionary<short, IDisposable> Cooldowns { get; set; }

        // Propiedades de personaje
        public byte IdSesion { get; set; }
        public long PuntosExperiencia { get; set; }


        #endregion

        #region Miembros

        private readonly object locker = new object();

        private Random _aleatoriedad;

        #endregion

        public GuerreroEntidadCombate(byte idSesion, string nombre, int hp, int mp, short mapa, short x, short y, byte nivel, long exp)
        {
            IdSesion = idSesion;
            Nombre = nombre;
            Hp = hp;
            Mp = mp;
            Mapa = mapa;
            X = x;
            Y = y;
            PuntosExperiencia = exp;
            ExperienciaQueOtorga = nivel * 100;
            Nivel = nivel;

            locker = new object();
            _aleatoriedad = new Random();
            BuffsObservables = new ConcurrentDictionary<short, IDisposable>();
            Cooldowns = new ConcurrentDictionary<short, IDisposable>();
        }
    }
}