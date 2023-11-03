using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexDescriptor<T> : IOGraphGdmVertexEntityDescriptor<T> 
    where T : class, new()
{
    private readonly GdmVertex<T> vertex;

    public GdmVertexDescriptor(GdmVertex<T> vertex)
    {
        this.vertex = vertex;
    }

    public IOGraphGdmEdgeDescriptor HasEdge(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmEdgeDescriptor<TVertex> HasEdge<TVertex>(Label label) where TVertex : class, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexEntityDescriptor<T> HasKey(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexEntityDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> expression) where TMember : struct
    {
        var propertyInfo = AssertExpression(expression);
        var entityType = vertex.Type.Definition;

        entityType.KeyResolver = instance =>
        {
            return propertyInfo.GetValue(instance)!;
        };

        return this;
    }

    public IOGraphGdmVertexEntityDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember?>> expression) where TMember : struct
    {
        var propertyInfo = AssertExpression(expression);
        var propertyName = new Label(propertyInfo.Name);
        var entityType = vertex.Type.Definition;

        var property = entityType.Properties[propertyName];

        

        entityType.KeyResolver = instance =>
        {
            return propertyInfo.GetValue(instance)!;
        };

        return this;
    }

    public IOGraphGdmVertexEntityDescriptor<T> HasLabel(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexEntityDescriptor<T> HasMetadata(Label label, MetaValue value)
    {
        vertex.Metadata[label] = value;
        return this;
    }

    public IOGraphGdmVertexEntityDescriptor<T> HasProperty(Label label, Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<TMember?> HasProperty<TMember>(Expression<Func<T, TMember?>> expression)
    {
        var propertyInfo = AssertExpression(expression);
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexEntityDescriptor<T> Ignore(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexEntityDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression)
    {
        var propertyInfo = AssertExpression(expression);
        var propertyName = propertyInfo.Name;
        var properties = vertex.Type.Definition.Properties;
        var property = properties.FirstOrDefault(p => p.Name == propertyName);

        if (property is not null)
        {
            properties.Remove(property);
        }

        return this;
    }


    private PropertyInfo AssertExpression<TMember>(Expression<Func<T, TMember>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (expression.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException("");
        }
        if (!memberExpression.Member.DeclaringType.IsAssignableTo(typeof(T)))
        {
            throw new Exception();
        }
        if (memberExpression.Member is not PropertyInfo propertyInfo)
        {
            throw new Exception();
        }
        return propertyInfo;
    }
}
