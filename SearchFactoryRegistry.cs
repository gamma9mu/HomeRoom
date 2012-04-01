using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    /// <summary>
    /// Provide a single, complete listing of every available search proider in
    /// and iterable format.
    /// </summary>
    public class SearchFactoryRegistry
    {
        /// <summary>
        /// The known search providers.
        /// </summary>
        private List<ISearchFactory> factories =
            new List<ISearchFactory>();

        /// <summary>
        /// The single instance of SearchFactoryRegistry.
        /// </summary>
        private static SearchFactoryRegistry singleton = null;

        /// <summary>
        /// The SearchFactoryRegistry constructor will take a StringCollection
        /// from the project app.config and load the classes.
        /// </summary>
        private SearchFactoryRegistry()
        {
            System.Collections.Specialized.StringCollection factoryNames =
                Properties.Settings.Default.SearchFactories;

            foreach (var factoryName in factoryNames){
                ISearchFactory factory = (ISearchFactory)
                    Type.GetType(factoryName).GetConstructor(new Type[]{})
                    .Invoke(null);
                if (factory != null)
                    factories.Add(factory);
            }
        }

        /// <summary>
        /// Get the only SearchFactoryRegistry object.
        /// </summary>
        /// <returns>The single instance of <code>SearchFactoryRegistry</code>.</returns>
        public static SearchFactoryRegistry getInstance()
        {
            if (singleton == null) singleton = new SearchFactoryRegistry();
            return singleton;
        }

        /// <summary>
        /// Get an ISearchFactory by name.
        /// </summary>
        /// <param name="name">The name of the ISEarchFactory.</param>
        /// <returns>The requested factory or null if it was not found.</returns>
        public static ISearchFactory getFactory(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an array of all the known ISearchFactory objects
        /// </summary>
        /// <returns>An ISearchFactory[] of all known search factories.</returns>
        public ISearchFactory[] getAllFactories()
        {
            return factories.ToArray();
        }
    }
}
