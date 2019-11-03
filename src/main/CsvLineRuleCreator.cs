using entities;
using model.CsvException;
using mxcd.core.rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace model
{
    public static class CsvLineRuleCreator
    {
        public static IEnumerable<IRule<ICsvLine>> Create(IEnumerable<ICondition> conditions)
        {
            var aResult = new List<CsvLineRule>();
            conditions = conditions ?? new List<Condition>();

            Func<ICsvLine, string, List<string>> getValues = (line, column) =>
            {
                var aValues = new List<string>();

                if (column != null)
                {
                    aValues.Add(line[column]);
                }
                else
                {
                    aValues = line.Values.ToList();
                }

                return aValues;
            };

            conditions.Select(x => x.condition).ToList().ForEach(x =>
            {
                if (!ConditionalOperators.GetEnum().Contains(x))
                {
                    throw new ConfigException($"Error en el archivo de configuración, el orperador {x} no exite" +
                        $"{Environment.NewLine}" +
                        $"Los operadores permitidos son:" +
                        $"{String.Join(" ,", ConditionalOperators.GetEnum().Select(y => y))}");
                }
            });

            foreach (var item in conditions)
            {
                switch (item.condition)
                {
                    case ConditionalOperators.equal:
                        aResult.Add(new CsvLineRule(x =>
                        {
                            foreach (var val in getValues(x, item.column))
                            {
                                var oEx = new ConditionException($"el registro {val} es difetente de {item.value}");

                                if (float.TryParse(val, out var fVar) && float.TryParse(item.value, out var fItem))
                                {
                                    if (fVar != fItem)
                                    {
                                        throw oEx;
                                    }
                                }
                                else
                                {
                                    if (val != item.value)
                                    {
                                        throw oEx;
                                    }
                                }
                            }

                            return true;
                        }));
                        break;

                    case ConditionalOperators.greater:
                        aResult.Add(new CsvLineRule(x =>
                        {
                            foreach (var val in getValues(x, item.column))
                            {
                                if (float.TryParse(val, out var fVar) && float.TryParse(item.value, out var fItem))
                                {
                                    if (fVar <= fItem)
                                    {
                                        throw new ConditionException($"el registro {val} es menor o igual que {item.value}");
                                    }
                                }
                                else
                                {
                                    throw new ConditionException($"el registro {val} no tiene formato de número para la comparación de >");
                                }
                            }

                            return true;
                        }));
                        break;

                    case ConditionalOperators.greaterEqual:
                        aResult.Add(new CsvLineRule(x =>
                        {
                            foreach (var val in getValues(x, item.column))
                            {
                                if (float.TryParse(val, out var fVar) && float.TryParse(item.value, out var fItem))
                                {
                                    if (fVar < fItem)
                                    {
                                        throw new ConditionException($"el registro {val} es menor que {item.value}");
                                    }
                                }
                                else
                                {
                                    throw new ConditionException($"el registro {val} no tiene formato de número para la comparación de >=");
                                }
                            }

                            return true;
                        }));
                        break;

                    case ConditionalOperators.minor:
                        aResult.Add(new CsvLineRule(x =>
                        {
                            foreach (var val in getValues(x, item.column))
                            {
                                if (float.TryParse(val, out var fVar) && float.TryParse(item.value, out var fItem))
                                {
                                    if (fVar >= fItem)
                                    {
                                        throw new ConditionException($"el registro {val} es mayor o igual que {item.value}");
                                    }
                                }
                                else
                                {
                                    throw new ConditionException($"el registro {val} no tiene formato de número para la comparación de <");
                                }
                            }

                            return true;
                        }));
                        break;


                    case ConditionalOperators.minorEqual:
                        aResult.Add(new CsvLineRule(x =>
                        {
                            foreach (var val in getValues(x, item.column))
                            {
                                if (float.TryParse(val, out var fVar) && float.TryParse(item.value, out var fItem))
                                {
                                    if (fVar > fItem)
                                    {
                                        throw new ConditionException($"el registro {val} es mayor que {item.value}");
                                    }
                                }
                                else
                                {
                                    throw new ConditionException($"el registro {val} no tiene formato de número para la comparación de >");
                                }
                            }

                            return true;
                        }));
                        break;
                    case ConditionalOperators.notEqual:
                        aResult.Add(new CsvLineRule(x =>
                        {
                            foreach (var val in getValues(x, item.column))
                            {
                                var oEx = new ConditionException($"el registro {val} es difetente de {item.value}");

                                if (float.TryParse(val, out var fVar) && float.TryParse(item.value, out var fItem))
                                {
                                    if (fVar == fItem)
                                    {
                                        throw oEx;
                                    }
                                }
                                else
                                {
                                    if (val == item.value)
                                    {
                                        throw oEx;
                                    }
                                }
                            }

                            return true;
                        }));
                        break;
                }
            }

            return aResult;
        }


    }
}
