#region

using System.Xml;
using System.Xml.Serialization;
using 多Xml文档建立CShape类.Extensions;
using 多Xml文档建立CShape类.Trees.CSharps;
using 多Xml文档建立CShape类.Trees.Xmls;

#endregion

var files = new List<string>();

files.AddRange(Directory.GetFiles(@"C:\Users\Zhou Taurus\Desktop\新建文件夹\hdevelop",
    "*",
    SearchOption.AllDirectories));

files.AddRange(Directory.GetFiles(@"C:\Users\Zhou Taurus\Desktop\新建文件夹\procedures",
    "*",
    SearchOption.AllDirectories));

var tree = new MultipleXmlDocumentNodeTree();
tree.Load(files.ToArray());
var cShapeTree = new CShapeTree();
cShapeTree.Load(tree, ArrayType.List);

// foreach (var classNode in cShapeTree.ClassNodes) classNode.CodeStr.WriteLine();
var serializer = new XmlSerializer(typeof(hdevelop));

foreach (var file in files)
{
    file.WriteLine();
    var o = serializer.Deserialize(new XmlTextReader(file));
    var d = (hdevelop) o;

    var enumerable = d.procedure.SingleOrDefault(x => x.name == "main")
        ?.body.Code.Select(o1 => ((IXmlRawContent) o1).RawContent!);

    if (enumerable != null)
        foreach (var s in enumerable)
            s.WriteLine();
}

Console.ReadKey();

public interface IXmlRawContent
{

    [XmlText]
    public string? RawContent { get; }

}

/// <summary>
/// hdevelop/procedure/interface/oc/par
/// </summary>
[XmlRoot("par")]
public class par : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("name")]
    public string? name { get; set; }
    [XmlAttribute("base_type")]
    public string? base_type { get; set; }
    [XmlAttribute("dimension")]
    public string? dimension { get; set; }

}

/// <summary>
/// hdevelop/procedure/interface/oc
/// </summary>
[XmlRoot("oc")]
public class oc : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("par")]
    public List<par>? par { get; set; }

}

/// <summary>
/// hdevelop/procedure/interface/ic/par
/// </summary>
[XmlRoot("par")]
public class hdevelop_procedure_interface_ic_par : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("name")]
    public string? name { get; set; }
    [XmlAttribute("base_type")]
    public string? base_type { get; set; }
    [XmlAttribute("dimension")]
    public string? dimension { get; set; }

}

/// <summary>
/// hdevelop/procedure/interface/ic
/// </summary>
[XmlRoot("ic")]
public class ic : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("par")]
    public List<hdevelop_procedure_interface_ic_par>?
        hdevelop_procedure_interface_ic_par { get; set; }

}

/// <summary>
/// hdevelop/procedure/interface/io/par
/// </summary>
[XmlRoot("par")]
public class hdevelop_procedure_interface_io_par : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("name")]
    public string? name { get; set; }
    [XmlAttribute("base_type")]
    public string? base_type { get; set; }
    [XmlAttribute("dimension")]
    public string? dimension { get; set; }

}

/// <summary>
/// hdevelop/procedure/interface/io
/// </summary>
[XmlRoot("io")]
public class io : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("par")]
    public List<hdevelop_procedure_interface_io_par>?
        hdevelop_procedure_interface_io_par { get; set; }

}

/// <summary>
/// hdevelop/procedure/interface/oo/par
/// </summary>
[XmlRoot("par")]
public class hdevelop_procedure_interface_oo_par : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("name")]
    public string? name { get; set; }
    [XmlAttribute("base_type")]
    public string? base_type { get; set; }
    [XmlAttribute("dimension")]
    public string? dimension { get; set; }

}

/// <summary>
/// hdevelop/procedure/interface/oo
/// </summary>
[XmlRoot("oo")]
public class oo : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("par")]
    public List<hdevelop_procedure_interface_oo_par>?
        hdevelop_procedure_interface_oo_par { get; set; }

}

/// <summary>
/// hdevelop/procedure/interface
/// </summary>
[XmlRoot("interface")]
public class @interface : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("oc")]
    public oc? oc { get; set; }
    [XmlElement("ic")]
    public ic? ic { get; set; }
    [XmlElement("io")]
    public io? io { get; set; }
    [XmlElement("oo")]
    public oo? oo { get; set; }

}

/// <summary>
/// hdevelop/procedure/body/c
/// </summary>
[XmlRoot("c")]
public class c : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("as_id")]
    public string? as_id { get; set; }
    [XmlAttribute("as_name")]
    public string? as_name { get; set; }
    [XmlAttribute("as_grp")]
    public string? as_grp { get; set; }
    [XmlAttribute("as_ord")]
    public string? as_ord { get; set; }

}

/// <summary>
/// hdevelop/procedure/body/l
/// </summary>
[XmlRoot("l")]
public class l : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("as_id")]
    public string? as_id { get; set; }
    [XmlAttribute("as_name")]
    public string? as_name { get; set; }
    [XmlAttribute("as_grp")]
    public string? as_grp { get; set; }
    [XmlAttribute("as_ord")]
    public string? as_ord { get; set; }

}

/// <summary>
/// hdevelop/procedure/body
/// </summary>
[XmlRoot("body")]
public class body : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

    // [XmlElement("c")]
    // public List<c>? c { get; set; }
    // [XmlElement("l")]
    // public List<l>? l { get; set; }
    [XmlElement("c", typeof(c))]
    [XmlElement("l", typeof(l))]
    public List<object> Code { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/default_type
/// </summary>
[XmlRoot("default_type")]
public class default_type : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/mixed_type
/// </summary>
[XmlRoot("mixed_type")]
public class mixed_type : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/sem_type
/// </summary>
[XmlRoot("sem_type")]
public class sem_type : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/type_list/item
/// </summary>
[XmlRoot("item")]
public class item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/type_list
/// </summary>
[XmlRoot("type_list")]
public class type_list : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("item")]
    public List<item>? item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/description
/// </summary>
[XmlRoot("description")]
public class description : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/multivalue
/// </summary>
[XmlRoot("multivalue")]
public class multivalue : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/default_value
/// </summary>
[XmlRoot("default_value")]
public class default_value : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/values/item
/// </summary>
[XmlRoot("item")]
public class hdevelop_procedure_docu_parameters_parameter_values_item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/values
/// </summary>
[XmlRoot("values")]
public class values : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("item")]
    public List<hdevelop_procedure_docu_parameters_parameter_values_item>?
        hdevelop_procedure_docu_parameters_parameter_values_item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/multichannel
/// </summary>
[XmlRoot("multichannel")]
public class multichannel : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/value_min
/// </summary>
[XmlRoot("value_min")]
public class value_min : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/value_list/item
/// </summary>
[XmlRoot("item")]
public class
    hdevelop_procedure_docu_parameters_parameter_value_list_item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/value_list
/// </summary>
[XmlRoot("value_list")]
public class value_list : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("item")]
    public List<hdevelop_procedure_docu_parameters_parameter_value_list_item>?
        hdevelop_procedure_docu_parameters_parameter_value_list_item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter/value_max
/// </summary>
[XmlRoot("value_max")]
public class value_max : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters/parameter
/// </summary>
[XmlRoot("parameter")]
public class parameter : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("id")]
    public string? id { get; set; }
    [XmlElement("default_type")]
    public default_type? default_type { get; set; }
    [XmlElement("mixed_type")]
    public mixed_type? mixed_type { get; set; }
    [XmlElement("sem_type")]
    public sem_type? sem_type { get; set; }
    [XmlElement("type_list")]
    public type_list? type_list { get; set; }
    [XmlElement("description")]
    public List<description>? description { get; set; }
    [XmlElement("multivalue")]
    public multivalue? multivalue { get; set; }
    [XmlElement("default_value")]
    public default_value? default_value { get; set; }
    [XmlElement("values")]
    public values? values { get; set; }
    [XmlElement("multichannel")]
    public multichannel? multichannel { get; set; }
    [XmlElement("value_min")]
    public value_min? value_min { get; set; }
    [XmlElement("value_list")]
    public value_list? value_list { get; set; }
    [XmlElement("value_max")]
    public value_max? value_max { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/parameters
/// </summary>
[XmlRoot("parameters")]
public class parameters : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("parameter")]
    public List<parameter>? parameter { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/short
/// </summary>
[XmlRoot("short")]
public class @short : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/chapters/item
/// </summary>
[XmlRoot("item")]
public class hdevelop_procedure_docu_chapters_item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/chapters
/// </summary>
[XmlRoot("chapters")]
public class chapters : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }
    [XmlElement("item")]
    public List<hdevelop_procedure_docu_chapters_item>?
        hdevelop_procedure_docu_chapters_item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/abstract
/// </summary>
[XmlRoot("abstract")]
public class @abstract : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/library
/// </summary>
[XmlRoot("library")]
public class library : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/alternatives/item
/// </summary>
[XmlRoot("item")]
public class hdevelop_procedure_docu_alternatives_item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/alternatives
/// </summary>
[XmlRoot("alternatives")]
public class alternatives : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("item")]
    public List<hdevelop_procedure_docu_alternatives_item>?
        hdevelop_procedure_docu_alternatives_item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/example
/// </summary>
[XmlRoot("example")]
public class example : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/predecessor/item
/// </summary>
[XmlRoot("item")]
public class hdevelop_procedure_docu_predecessor_item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/predecessor
/// </summary>
[XmlRoot("predecessor")]
public class predecessor : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("item")]
    public List<hdevelop_procedure_docu_predecessor_item>?
        hdevelop_procedure_docu_predecessor_item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/see_also/item
/// </summary>
[XmlRoot("item")]
public class hdevelop_procedure_docu_see_also_item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/see_also
/// </summary>
[XmlRoot("see_also")]
public class see_also : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("item")]
    public List<hdevelop_procedure_docu_see_also_item>?
        hdevelop_procedure_docu_see_also_item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/successor/item
/// </summary>
[XmlRoot("item")]
public class hdevelop_procedure_docu_successor_item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/successor
/// </summary>
[XmlRoot("successor")]
public class successor : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("item")]
    public List<hdevelop_procedure_docu_successor_item>?
        hdevelop_procedure_docu_successor_item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/attention
/// </summary>
[XmlRoot("attention")]
public class attention : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/keywords/item
/// </summary>
[XmlRoot("item")]
public class hdevelop_procedure_docu_keywords_item : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/keywords
/// </summary>
[XmlRoot("keywords")]
public class keywords : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }
    [XmlElement("item")]
    public List<hdevelop_procedure_docu_keywords_item>?
        hdevelop_procedure_docu_keywords_item { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/warning
/// </summary>
[XmlRoot("warning")]
public class warning : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu/references
/// </summary>
[XmlRoot("references")]
public class references : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/procedure/docu
/// </summary>
[XmlRoot("docu")]
public class docu : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("id")]
    public string? id { get; set; }
    [XmlElement("parameters")]
    public parameters? parameters { get; set; }
    [XmlElement("short")]
    public List<@short>? @short { get; set; }
    [XmlElement("chapters")]
    public List<chapters>? chapters { get; set; }
    [XmlElement("abstract")]
    public List<@abstract>? @abstract { get; set; }
    [XmlElement("library")]
    public List<library>? library { get; set; }
    [XmlElement("alternatives")]
    public alternatives? alternatives { get; set; }
    [XmlElement("example")]
    public List<example>? example { get; set; }
    [XmlElement("predecessor")]
    public predecessor? predecessor { get; set; }
    [XmlElement("see_also")]
    public see_also? see_also { get; set; }
    [XmlElement("successor")]
    public successor? successor { get; set; }
    [XmlElement("attention")]
    public attention? attention { get; set; }
    [XmlElement("keywords")]
    public List<keywords>? keywords { get; set; }
    [XmlElement("warning")]
    public List<warning>? warning { get; set; }
    [XmlElement("references")]
    public references? references { get; set; }

}

/// <summary>
/// hdevelop/procedure
/// </summary>
[XmlRoot("procedure")]
public class procedure : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("name")]
    public string? name { get; set; }
    [XmlAttribute("access")]
    public string? access { get; set; }
    [XmlElement("interface")]
    public @interface? @interface { get; set; }
    [XmlElement("body")]
    public body? body { get; set; }
    [XmlElement("docu")]
    public docu? docu { get; set; }

}

/// <summary>
/// hdevelop/library/docu/short
/// </summary>
[XmlRoot("short")]
public class hdevelop_library_docu_short : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("lang")]
    public string? lang { get; set; }

}

/// <summary>
/// hdevelop/library/docu
/// </summary>
[XmlRoot("docu")]
public class hdevelop_library_docu : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("short")]
    public hdevelop_library_docu_short? hdevelop_library_docu_short { get; set; }

}

/// <summary>
/// hdevelop/library
/// </summary>
[XmlRoot("library")]
public class hdevelop_library : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlElement("docu")]
    public hdevelop_library_docu? hdevelop_library_docu { get; set; }

}

/// <summary>
/// hdevelop
/// </summary>
[XmlRoot("hdevelop")]
public class hdevelop : IXmlRawContent
{

    [XmlText()]
    public string? RawContent { get; set; }
    [XmlAttribute("file_version")]
    public string? file_version { get; set; }
    [XmlAttribute("halcon_version")]
    public string? halcon_version { get; set; }
    [XmlElement("procedure")]
    public List<procedure>? procedure { get; set; }
    [XmlElement("library")]
    public hdevelop_library? hdevelop_library { get; set; }

}