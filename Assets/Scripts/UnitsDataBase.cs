using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// База данных всех юнитов в игре
/// </summary>
public class UnitsDataBase : MonoBehaviour
{
    private static UnityEventUnit onAddNewUnit = new UnityEventUnit();
    private static UnityEventUnit onRemoveUnit = new UnityEventUnit();

    private static UnityEventUnit onSearchUnit = new UnityEventUnit();
    private static UnityEventString onSearchUnitBy = new UnityEventString();

    private static string searchId = string.Empty;
    private static GameObject searchObject = null;

    private List<IIdentitable> units = new List<IIdentitable>();

    private void Awake()
    {
        onAddNewUnit.AddListener(AddNewUnitHandler);
        onRemoveUnit.AddListener(RemoveUnitHandler);
        onSearchUnit.AddListener(SearchUnitIdHandler);
        onSearchUnitBy.AddListener(SearchUnitGOHandler);
    }

    private void OnDestroy()
    {
        onAddNewUnit.RemoveListener(AddNewUnitHandler);
        onRemoveUnit.RemoveListener(RemoveUnitHandler);
        onSearchUnit.RemoveListener(SearchUnitIdHandler);
        
        onSearchUnitBy.RemoveListener(SearchUnitGOHandler);
    }

    public static void AddNewUnit(GameObject newUnit)
    {
        onAddNewUnit.Invoke(newUnit);
    }

    public static void RemoveUnit(GameObject unit)
    {
        onRemoveUnit.Invoke(unit);
    }

    public static string GetMyId(GameObject unit)
    {

        onSearchUnit.Invoke(unit);
        return searchId;
    }

    public static GameObject GetUnit(string id)
    {

        onSearchUnitBy.Invoke(id);
        return searchObject;
    }

    private void AddNewUnitHandler(GameObject newUnit)
    {
        units.Add(newUnit.GetComponent<IIdentitable>());
    }

    private void RemoveUnitHandler(GameObject unit)
    {
        units.Remove(unit.GetComponent<IIdentitable>());
    }

    private void SearchUnitIdHandler(GameObject unit)
    {
        int index = 0;
        foreach (var item in units)
        {
            if(unit == item.GetGameObject )
            {
                break;
            }
            index += 1;
        }
        searchId = index.ToString();
    }
    private void SearchUnitGOHandler(string unit)
    {
        foreach (var item in units)
        {
           if( item.Id == unit)
            {
                searchObject = item.GetGameObject;
                break;
            }
        }
    }
}


public class UnityEventUnit : UnityEvent<GameObject> { };

public class UnityEventString : UnityEvent<string> { };