# jbanana.boot

## Mapeament CRUD -> REST -> (Persistente(P)|Transiente(T)|Synch(S)|Asynch(A))
* Create  -> POST     -> PS
* Restore -> GET      -> TA
* Update  -> PUT      -> PS
* Update  -> PATCH    -> PS
* Delete  -> DELETE   -> PS

## Rest Tradicional
* HttpRequest -> IOC -> DAO* 
* *=(Persistência: SQL -> DB)
* Aplicação Persistente

## Rest JBanana
* HttpRequest* -> IOC -> XPATH -> OO 
* *=(Persistência: Request != GET, Prevayler)
* Application Server Persistente

## Exemplo OO
* 1 Phonebook -> * Contact -> * Phone

Create (POST)
--------------
* ./phonebook/contacts/               -> Contact create(json)
* ./phonebook/contact/id/{?}/phones,  -> Phone create(json, "phonebook/contacts[id=?]/phones")

Restore (GET)
--------------
* ./phonebook/contacts                -> Contact[] restoreAll("phonebook/contacts")
* ./phonebook/contacts/id/?           -> Contact restoreById("phonebook/contacts[id=?]",id)
* ./phonebook/contacts/name/?         -> Contact[] restoreByName("phonebook/contacts[name=?]",name)
* ./phonebook/contacts/name/like(?)   -> Contact[] restoreByName("phonebook/contacts[name=like(?)], name)

* ./phonebook/contact/id/?/phones               -> Phone[] restoreAll("phonebook/contacts[id=?]/phones", id)
* ./phonebook/contact/id/?/phones/id/?          -> Phone restoreById("phonebook/contacts[id=?]/phones[id=?]", id1, id2) 
* ./phonebook/contact/id/?/phones/type/?        -> Phone[] restoreByType("phonebook/contacts[id=?]/phones[type=?]", id, type)
* ./phonebook/contact/id/?/phones/name/?        -> Phone[] restoreByName("phonebook/contacts[id=?]/phones[name=?]", id, name)

Full Update (PUT)
--------------
* ./phonebook/contacts/id/?              -> Contact update(json, "phonebook/contacts", id)
* ./phonebook/contact/id/?/phones/id/?   -> Phone update(json, "phonebook/contacts[id=?]/phones[id=?]", id1, id2)

Partial Update (PATCH)
--------------
* ./phonebook/contacts/id/?              -> Contact update(json, "phonebook/contacts", id)
* ./phonebook/contact/id/?/phones/id/?   -> Phone update(json, "phonebook/contacts[id=?]/phones[id=?]", id1, id2)

Delete (DELETE)
----------------
* ./phonebook/contacts/id/?             -> boolean delete("phonebook/contacts[id=?]", id)
* ./phonebook/contact/id/?/phones/id/?  -> boolean delete("phonebook/contacts[id=?]/phones[id=?]", id1, id2)

# Links
http://swagger.io/
