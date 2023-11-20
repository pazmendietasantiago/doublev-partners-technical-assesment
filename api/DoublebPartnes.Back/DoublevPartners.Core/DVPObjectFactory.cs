namespace DoublebPartnes.Core;

public class DVPObjectFactory
{
    private static readonly object objectSync = new();
    private static readonly SortedList<string, object> factories = new();
    private static readonly SortedList<string, object> factoryCache = new();

    /// <summary>
    ///     Registrar una función de construcción de un objeto
    /// </summary>
    /// <typeparam name="TR">Tipo del objeto</typeparam>
    /// <param name="factory">Constructor del objeto</param>
    public static void RegisterFactory<TR>(Func<TR> factory)
    {
        Add<TR>(factory);
    }

    /// <summary>
    ///     Registrar una función de construcción de un objeto
    /// </summary>
    /// <typeparam name="T1">Tipo del parámetro para el constructor el objeto</typeparam>
    /// <typeparam name="TR">Tipo del objeto</typeparam>
    /// <param name="factory">Constructor del objeto</param>
    public static void RegisterFactory<T1, TR>(Func<T1, TR> factory)
    {
        Add<TR>(factory);
    }

    /// <summary>
    ///     Crear una instancia del objeto
    /// </summary>
    /// <typeparam name="TR">Tipo del objeto a instanciar</typeparam>
    /// <param name="useCache">True, use cache, False, siempre cree una nueva intancia</param>
    /// <returns>Instancia del objeto</returns>
    public static TR Create<TR>(bool useCache = true) where TR : new()
    {
        if (useCache)
            return GetFromCache<TR>();
        return new TR();
    }

    /// <summary>
    ///     Crear una instancia del objeto
    /// </summary>
    /// <typeparam name="T1">Tipo del parámetro para el constructor el objeto</typeparam>
    /// <typeparam name="TR">Tipo del objeto a instanciar</typeparam>
    /// <param name="t1">Parámetro para el constructor del objeto</param>
    /// <param name="useCache">True, use cache, False, siempre cree una nueva intancia</param>
    /// <returns>Instancia del objeto</returns>
    public static TR Create<T1, TR>(T1 t1, bool useCache = true)
    {
        object factory;
        object instance;

        if (useCache && factoryCache.TryGetValue(typeof(TR).FullName, out instance)) return (TR)instance;

        if (factories.TryGetValue(typeof(TR).FullName, out factory))
        {
            var result = ((Func<T1, TR>)factory).Invoke(t1);

            if (useCache)
            {
                lock (objectSync)
                {
                    factoryCache.Add(typeof(TR).FullName, result);
                }

                return result;
            }
        }

        return default;
    }

    private static void Add<TR>(object factory)
    {
        if (!factories.ContainsKey(typeof(TR).FullName)) factories.Add(typeof(TR).FullName, factory);
    }

    private static TR GetFromCache<TR>() where TR : new()
    {
        object instance;
        object factory;

        lock (objectSync)
        {
            if (factoryCache.TryGetValue(typeof(TR).FullName, out instance)) return (TR)instance;

            if (factories.TryGetValue(typeof(TR).FullName, out factory))
            {
                var result = ((Func<TR>)factory).Invoke();

                lock (objectSync)
                {
                    factoryCache.Add(typeof(TR).FullName, result);
                }

                return result;
            }
            else
            {
                var result = new TR();
                factoryCache.Add(typeof(TR).FullName, result);
                return result;
            }
        }
    }
}