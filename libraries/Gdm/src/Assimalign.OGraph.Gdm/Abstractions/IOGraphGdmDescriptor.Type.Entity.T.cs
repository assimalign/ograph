using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEntityTypeDescriptor<T> where T : class, new()
{
    IOGraphGdmEntityTypeDescriptor<T> HasLabel(Label label);
    IOGraphGdmEntityTypeDescriptor<T> HasKey(IOGraphGdmEntityKey key);
    IOGraphGdmEntityTypeDescriptor<T> HasKey(Func<IOGraphGdmEntityType, IOGraphGdmEntityKey> func);
    IOGraphGdmEntityTypeDescriptor<T> HasProperty(IOGraphGdmProperty property);
    IOGraphGdmEntityTypeDescriptor<T> HasProperty(Func<IOGraphGdmEntityType, IOGraphGdmProperty> func);
    IOGraphGdmEntityTypeDescriptor<T> HasFunction(IOGraphGdmFunction function);
    IOGraphGdmEntityTypeDescriptor<T> HasFunction(Func<IOGraphGdmEntityType, IOGraphGdmFunction> func);
    IOGraphGdmEntityTypeDescriptor<T> AddMeta(string key, string value);



    IOGraphGdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey>> expression) where TKey : struct;
    IOGraphGdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey?>> expression) where TKey : struct;
    IOGraphGdmPropertyDescriptor<TProperty?> HasProperty<TProperty>(Expression<Func<T, TProperty?>> expression);
    IOGraphGdmFunctionDescriptor<TFunction?> HasFunction<TFunction>(Expression<Func<T, TFunction?>> expression);
}