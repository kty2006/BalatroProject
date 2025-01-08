public interface IEvent
{
    public void Execute();
}
public interface ITurnObj
{
    public void Invoke();
}

public interface UnitType
{
    public void Execute();
}

public interface IAttack : UnitType
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
