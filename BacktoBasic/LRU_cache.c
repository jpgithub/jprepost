#include <stdio.h>
#include <stdlib.h>

typedef struct
{
	int key;
	int value;
}KeyValuePair;

typedef struct {
    KeyValuePair *data;
    int size;
    int recent_rw;
	int occupied;
} LRUCache;


inline int hashfunction(int key,int size) {
    return key % size;
}

LRUCache* lRUCacheCreate(int capacity){
    LRUCache* cache_obj = malloc(1*sizeof(LRUCache));
    cache_obj->data = malloc(capacity * sizeof(KeyValuePair));
    cache_obj->size = capacity;
    for (int i = 0; i < capacity; i++)
    {
        cache_obj->data[i].key = -1;
        cache_obj->data[i].value = -1;
    }
    cache_obj->recent_rw = -1;
	cache_obj->occupied = 0;
    return cache_obj;
}

int lRUCacheGet(LRUCache* obj, int key) {
	int found = -1;
	int i = hashfunction(key, obj->size);
    //for (int i = 0; i < obj->size; i++)
	//{
	    if (obj->data[i].key == key)
	    {
	        found = obj->data[i].value;
			obj->recent_rw = i;
	        //break;
	    }
		else{
			// Collision
		}
	//}
	return found;
}

void lRUCachePut(LRUCache* obj, int key, int value) {
	// eviction and replacement
	int index = hashfunction(key, obj->size);
	if (obj->occupied == obj->capacity)
    {
		;
	}
	else
	{	
		if (obj->data[index].key == -1)
		{
			obj->data[index].key = key;
			obj->data[index].value = value;
			obj->occupied++;
			obj->recent_rw = index;
		}
		else{
			//Index Collision Resolution
			lRUCachePut(obj, key + 1, value);
			// It not possible to have infinite recursion
		}
		
		/*
		int i = 0;
		if (obj->recent_rw > -1)
		{
			i = obj->recent_rw;
		}
		for (; i < obj->size ; i++)
		{
			if (obj->data[i].key == -1)
			{
				obj->data[i].key = key;
				obj->data[i].value = value;
				obj->occupied++;
				obj->recent_rw = i;
				break;
			}
		}*/
	}
}

void lRUCacheFree(LRUCache* obj) {
    free(obj->data);
    free(obj);
}

int main()
{
    LRUCache* myobj = lRUCacheCreate(2);
    lRUCacheFree(myobj);
}