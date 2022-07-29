using System;

namespace Editor.CodeGenerator {
/// <summary>
/// Provides a constructor for the T4 template to pass the data in.
/// </summary>
public partial class StringItemsGenerator {
    /// <summary>
    /// Stores the generated class name.
    /// </summary>
    private readonly string m_className;

    /// <summary>
    /// A datasource for which concrete static fields will be created on the generated class.
    /// </summary>
    private readonly string[] m_dataSource;

    /// <summary>
    /// Initializes a new instance of the Generator class.
    /// </summary>
    /// <param name="generatedClassName"></param>
    /// <param name="dataSource"></param>
    public StringItemsGenerator(string generatedClassName, string[] dataSource) {
        if (string.IsNullOrEmpty(generatedClassName)) {
            throw new ArgumentException("generatedClassName");
        }

        if (dataSource == null) {
            throw new ArgumentNullException("source cannot be null!");
        }

        this.m_className  = generatedClassName;
        this.m_dataSource = dataSource;
    }
}
}