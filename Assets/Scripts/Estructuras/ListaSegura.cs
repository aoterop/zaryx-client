using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Zaryx_Game.Estructuras
{
    public class ListaSegura<T> : IDisposable, IEnumerable<T>
    {
        private readonly List<T> _elementos;
        private readonly ReaderWriterLockSlim _locker;
        private bool _reciclado;

        public ListaSegura()
        {
            _elementos = new List<T>();
            _locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _reciclado = false;
        }

        public ListaSegura(List<T> elementos)
        {
            _elementos = new List<T>(elementos);
            _locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _reciclado = false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    List<T> copia = new(_elementos);
                    return copia.GetEnumerator();
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return Enumerable.Empty<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(T elemento)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.Contains(elemento);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return false;
        }

        public int Count
        {
            get
            {
                if (!_reciclado)
                {
                    _locker.EnterReadLock();
                    try
                    {
                        return _elementos.Count;
                    }
                    finally
                    {
                        _locker.ExitReadLock();
                    }
                }
                return 0;
            }
        }

        public void Add(T elemento)
        {
            if (!_reciclado)
            {
                _locker.EnterWriteLock();
                try
                {
                    _elementos.Add(elemento);
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public void AddRange(List<T> elementos)
        {
            if (!_reciclado)
            {
                _locker.EnterWriteLock();
                try
                {
                    _elementos.AddRange(elementos);
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public bool All(Func<T, bool> predicado)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.All(predicado);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return false;
        }

        public bool Any(Func<T, bool> predicado)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.Any(predicado);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return false;
        }

        public void Clear()
        {
            if (!_reciclado)
            {
                _locker.EnterWriteLock();
                try
                {
                    _elementos.Clear();
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public void CopyTo(T[] elementos)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    _elementos.CopyTo(elementos);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
        }

        public int CountLinq(Func<T, bool> predicado)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.Count(predicado);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return 0;
        }

        public T ElementAt(int pos)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos[pos];
                }
                catch
                {
                    Console.WriteLine("Se ha producido un error al intentar acceder al elemento en la posición " + pos);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return default;
        }

        public T Find(Predicate<T> predicado)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.Find(predicado);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return default;
        }

        public T FirstOrDefault()
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.FirstOrDefault();
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return default;
        }

        public void ForEach(Action<T> accion)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    _elementos.ForEach(accion);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
        }

        public List<T> GetAllItems()
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return new List<T>(_elementos);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return new List<T>();
        }

        public T LastOrDefault(Func<T, bool> predicado)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.LastOrDefault(predicado);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return default;
        }

        public void Remove(T elemento)
        {
            if (!_reciclado)
            {
                _locker.EnterWriteLock();
                try
                {
                    _elementos.Remove(elemento);
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public void RemoveAt(int index)
        {
            if (!_reciclado)
            {
                _locker.EnterWriteLock();
                try
                {
                    if (index >= 0 && index < _elementos.Count)
                    {
                        _elementos.RemoveAt(index);
                    }
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public void RemoveAll(Predicate<T> coincidencia)
        {
            if (!_reciclado)
            {
                _locker.EnterWriteLock();
                try
                {
                    _elementos.RemoveAll(coincidencia);
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public T Single(Func<T, bool> predicado)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.Single(predicado);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return default;
        }

        public T SingleOrDefault(Func<T, bool> predicado)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.SingleOrDefault(predicado);
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return default;
        }

        public List<T> Where(Func<T, bool> predicado)
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();
                try
                {
                    return _elementos.Where(predicado).ToList();
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            return new List<T>();
        }

        public List<T> ToList()
        {
            if (!_reciclado)
            {
                _locker.EnterReadLock();

                try
                {
                    return _elementos.ToList();
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }

            return new List<T>();
        }

        public void Dispose()
        {
            if (!_reciclado)
            {// Si no se ha reciclado.
                _reciclado = true;
                Reciclar(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Reciclar(bool seRecicla)
        {
            if (seRecicla)
            {
                Clear();
                _locker.Dispose();
            }
        }
    }
}