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

static inline int hashfunction(int key,int size) {
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
	// Reset recent_rw to base zero if found is -1
	obj->recent_rw = 0;
	int index = hashfunction(key,obj->size);
	int k = key;
	int i = 0; // Worst case search entire obj->size
	while(i < obj->size)
	{
		if (obj->data[index].key == key)
		{
			found = obj->data[index].value;
			obj->recent_rw = index;
			break;
		}
		k++;
		index = hashfunction(k, obj->size);
		i++;
	}
	return found;
}

void lRUCachePut(LRUCache* obj, int key, int value) {
	int index = hashfunction(key, obj->size);
	int k = key;
	int i = 0;
	
	while(i < obj->size)
	{
		if (obj->occupied == obj->size)
		{
			// Evict and replacement
			if (index != obj->recent_rw)
			{
				obj->data[index].key = key;
				obj->data[index].value = value;
				obj->recent_rw = index;
				break;
			}
		}
		else
		{
			// Populated
			if (obj->data[index].key == -1)
			{
				obj->data[index].key = key;
				obj->data[index].value = value;
				obj->occupied++;
				obj->recent_rw = index;
				break;
			}
		}
		k++;
		index = hashfunction(k, obj->size);
		i++;
	}
}

void lRUCacheFree(LRUCache* obj) {
    free(obj->data);
    free(obj);
}


static inline void dump_cache(LRUCache* myobj)
{
        printf("\nCache:\n{");
        for (int i = 0; i < myobj->size; i++)
        {
                printf("%d=%d,", myobj->data[i].key, myobj->data[i].value);
        }
        printf("}\n");
}

int main()
{
    LRUCache* myobj = lRUCacheCreate(2);
	lRUCachePut(myobj,1,1);
	lRUCachePut(myobj,2,2);
	
	dump_cache(myobj);
	
	int ans = lRUCacheGet(myobj,1);
	printf("\n %d", ans);
	
	lRUCachePut(myobj,3,3);
	
	ans = lRUCacheGet(myobj,2);
	printf("\n %d", ans);
	
	lRUCachePut(myobj,4,4);
	
	dump_cache(myobj);
	
	ans = lRUCacheGet(myobj, 1);
	printf("\n %d", ans);
	ans = lRUCacheGet(myobj,3);
	printf("\n %d", ans);
	ans = lRUCacheGet(myobj,4);
	printf("\n %d", ans);
    
	lRUCacheFree(myobj);
}