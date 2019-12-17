using entities;
using model.CsvException;
using mxcd.core.rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace model
{
    public static class CsvActionCreator
    {
        public static IEnumerable<Action<ICsvLine>> Create(IEnumerable<ITupleReplace> replaces)
        {
            var result = new List<Action<ICsvLine>>();
            foreach (var item in replaces)
            {
                result.Add(x =>
                {
                    if (item.column == null)
                    {
                        foreach(var val in x.Keys.ToList())
                        {
                            x[val] = x[val].Replace(item.from, item.to);
                        }
                    }
                    else
                    {
                        x[item.column] = x[item.column].Replace(item.from, item.to);
                    }
                });
            }

            return result;
        }

        public static IEnumerable<Action<ICsvLine>> Create(IEnumerable<ITypeColumn> types)
        {
            var result = new List<Action<ICsvLine>>();

            foreach (var item in types)
            {

                result.Add(x =>
                {
                    var value = false;
                    
                    if (item.column == null)
                    {
                        foreach(var val in x)
                        {
                            switch (item.type)
                            {
                                case ETypeColumn.number:
                                    var str = x[val.Key].Replace(",", "").Replace(".", "");
                                    value = long.TryParse(str, out var number);
                                    break;
                                default:
                                    value = true;
                                    break;
                            }

                            if (!value)
                            {
                                throw new FormatException($"Posición({val.Key}): No se cumple el formato indicado");
                            }
                        }
                    }
                    else
                    {
                        switch (item.type)
                        {
                            case ETypeColumn.number:
                                var str = x[item.column].Replace(",", "").Replace(".", "");
                                value = long.TryParse(str, out var number);
                                break;
                            default:
                                value = true;
                                break;
                        }

                        if (!value)
                        {
                            throw new FormatException("No se cumple el formato indicado");
                        }

                    }



                });
            }

            return result;
        }
    }
}
