using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmComplexTypeDescriptor<T> where T : class, new()
{
    IOGraphGdmComplexTypeDescriptor<T> HasLabel(Label label);
    IOGraphGdmComplexTypeDescriptor<T> HasFunction(IOGraphGdmFunction function);
    IOGraphGdmComplexTypeDescriptor<T> HasFunction(Func<IOGraphGdmComplexType, IOGraphGdmFunction> func);
    IOGraphGdmComplexTypeDescriptor<T> HasProperty(IOGraphGdmProperty property);
    IOGraphGdmComplexTypeDescriptor<T> HasProperty(Func<IOGraphGdmComplexType, IOGraphGdmProperty> func);
    IOGraphGdmComplexTypeDescriptor<T> AddMeta(string key, string value);


    IOGraphGdmPropertyDescriptor<TProperty?> HasProperty<TProperty>(Expression<Func<T, TProperty?>> expression);
    IOGraphGdmFunctionDescriptor<TFunction?> HasFunction<TFunction>(Expression<Func<T, TFunction?>> expression);
}