public interface IEvent
{
    public void Execute();
}
public interface ITurnObj
{
    public bool Invoke();
}

public interface Observer
{
    public void Update();
}

public interface UnitBehaviour
{
    public void Invoke();
}

public interface IAttack : UnitBehaviour
{

}

public interface IMove : UnitBehaviour
{

}



#region //Factory∆–≈œ
public interface IProduct
{
    public void Setting();
}

public interface Ifactory //
{
    IProduct SomeOperation()
    {
        IProduct product = CreateProduct();
        product.Setting();
        return product;
    }

    public IProduct CreateProduct();
}
#endregion
