package or.cuidabrasil.core.dao;

import java.util.HashMap;
import java.util.Map;

public class DAOFactory {


    //region <---- SINGLETON ---->

    private static DAOFactory instance;

    protected static DAOFactory getInstance() {
        if(instance == null)
            instance = new DAOFactory();
        return instance;
    }

    //endregion

    private final Map<Class<?>, IGenericDAO>  daos = new HashMap<>();



    public static <T extends IGenericDAO> T getDao(Class<? extends IGenericDAO> clazz)
            throws IllegalAccessException, ClassNotFoundException, InstantiationException {
        return getInstance()._getDao(clazz);
    }

    private <T extends IGenericDAO> T _getDao(Class<? extends IGenericDAO> clazz)
            throws InstantiationException, IllegalAccessException, ClassNotFoundException {

        IGenericDAO dao = daos.get(clazz);
        if(dao != null)
            return (T) dao;

        return instantiateAndRegistry(clazz);
    }

    private <T extends IGenericDAO> T instantiateAndRegistry(Class<? extends IGenericDAO> clazz)
            throws ClassNotFoundException, InstantiationException, IllegalAccessException {

        if (!clazz.isInterface())
            throw new IllegalArgumentException(clazz.getName() + " is not a Interface!");

        Class<?> key = clazz;
        clazz = (Class<T>) Class.forName(clazz.getName() + "Impl");
        return (T) instantiate(clazz, key);
    }

    public static void registry(IGenericDAO dao) { getInstance()._registry(dao); }
    private void _registry(IGenericDAO dao) {

        Class<?> key = findDaoInterfaceInterface(dao.getClass());
        if (!key.isInterface())
            throw new IllegalArgumentException(key.getName() + " is not a Interface!");

        daos.put(key, dao);
    }

    private <T extends IGenericDAO> T instantiate(Class<T> clazz, Class<?> key)
            throws IllegalAccessException, InstantiationException {

        T result = clazz.newInstance();
        daos.put(key, result);
        return result;
    }

    private Class<?> findDaoInterfaceInterface(Class<?> clazz) {
        Class<?>[] interfaces = clazz.getInterfaces();
        for (Class<?> candidate : interfaces)
            if (candidate.getAnnotation(DefaultDAO.class) != null)
                return candidate;

        throw new IllegalArgumentException("none default interface founded (" + clazz.getName() + ")");
    }

}
