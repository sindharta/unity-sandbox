using System.Text;

public static class StringBuilderExtensions {

    public static void AppendStrings(this StringBuilder builder, params string[] strings) {

        for (int i=0; i < strings.Length; ++i) {
            builder.Append(strings[i]);
        }
    }

}
