[![Build Status](https://travis-ci.org/BenVlodgi/VMFParser.svg)](https://travis-ci.org/BenVlodgi/VMFParser)
# VMFParser

.Net Library written in C#. Useful for generating or parsing in an existing VMF into a tree like structure. Tree can be written to a new [VMF file](https://developer.valvesoftware.com/wiki/Valve_Map_Format).

# API
- [namespace `VMFParser`](#namespace-vmfparser)
  - [interface `IVNode`](#interface-ivnode)
    - [string `Name` (get)](#string-name-get)
  - [class `VBlock` : `IVNode`](#class-vblock--ivnode)
      - [string `Name` (get, private set)](#string-name-get-private-set)
      - [IList\<`IVNode`\> (get, private set)](#ilist-body-get-private-set)
      - [Constructor(string `name`, IList\<IVNode\> `body`(null))](#constructorstring-name-ilist-bodynull)
      - [Constructor(string[] text)](#constructorstring-text)
      - [string[] `ToVMFStrings`(bool useTabs(true))](#string-tovmfstringsbool-usetabstrue)
      - [string `ToString`()](#string-tostring)
  - [`class VProperty` : `IVNode`](#class-vproperty--ivnode)
      - [string `Name` (get, private set)](#string-name-get-private-set-1)
      - [string `Value` (get, set)](#string-value-get-set)
      - [Constructor(string name, string value)](#constructorstring-name-string-value)
      - [Constructor(string text)](#constructorstring-text-1)
      - [string `ToVMFString`()](#string-tovmfstring)
      - [string `ToString`()](#string-tostring-1)
  - [class `VMF`](#class-vmf)
      - [IList\<`IVNode`\> `Body` (get, private set)](#ilist-body-get-private-set-1)
      - [Constructor()](#constructor)
      - [Constructor(IList\<`IVNode`\> body)](#constructorilist-body)
      - [Constructor(string[] text)](#constructorstring-text-2)
      - [string[] `ToVMFStrings()`](#string-tovmfstrings)


<br />

## namespace `VMFParser`
### interface `IVNode`
- Represents an entry in a `VMF`.
##### string `Name` (get)
- Represents the title of a node.


<br /><br />

### class `VBlock` : `IVNode`
- Represents a block containing other `IVNode`s in a `VMF`

In the following Example, `Name` = **visgroup**, and `Body` will contain a list of the `VPropery`s

    visgroup
    {
      "name" "Tree_1"
      "visgroupid" "5"
      "color" "65 45 0"
    }

###### string `Name` (get, private set)
- Represents the title of the Body, it is the top-level text before the `{ }`.
###### IList\<`IVNode`\> `Body` (get, private set)
- Holds subnodes of this `VBlock`.
###### Constructor(string `name`, IList\<IVNode\> `body`(null))
- Initializes a new instance of the `VBlock` class from its name and a list of `IVNode`s.
###### Constructor(string[] text)
- Initializes a new instance of the `VBlock` class from VMF text.
###### string[] `ToVMFStrings`(bool useTabs(true))
- Generates the VMF text representation of this block.
###### string `ToString`()
- Returns base ToString with "(Name)" to help with debugging.


<br /><br />

### class `VProperty` : `IVNode`
- Represents a single property name and value.

In the following example, `Name` = **visgroupid**, and the `Value` = **5**

    "visgroupid" "5"

###### string `Name` (get, private set)
- Represents the title/name of the property.
###### string `Value` (get, set)
- Holds the text value of this property.
###### Constructor(string name, string value)
- Initializes a new instance of the `VProperty` class from the name and value of a property.
###### Constructor(string text)
- Initializes a new instance of the `VProperty` class from vmf text line.
###### string `ToVMFString`()
- Generates the VMF text representation of this Property.
###### string `ToString`()
- Returns base ToString with "(`Name`)" to help with debugging.


<br /><br />

### class `VMF`
- Represents the full contents of a [VMF file](https://developer.valvesoftware.com/wiki/Valve_Map_Format), this is similar to a `VBLock`, but with no `Name`.
###### IList\<`IVNode`\> `Body` (get, private set)
- Holds all the subnodes (`IVNode`) in a `VMF`
###### Constructor()
- Initializes a new instance of the `VMF` class.
###### Constructor(IList\<`IVNode`\> body)
- Initializes a new instance of the `VMF` class from a list of IVNodes.
###### Constructor(string[] text)
- Initializes a new instance of the `VMF` class from the VMF text.
###### string[] `ToVMFStrings()`
- Generates the VMF text from the body of `IVNode`s.





