


typedef struct {
    int *key;
    int *value;
} LRUCache;


LRUCache* lRUCacheCreate(int capacity){
    LRUCache* cache_obj = malloc(1*sizeof(LRUCache));
    cache_obj.key = calloc(capacity , sizeof(int));
    cache_obj.value = calloc(capacity, sizeof(int));
    return cache_obj;
}

int lRUCacheGet(LRUCache* obj, int key) {
    
}

void lRUCachePut(LRUCache* obj, int key, int value) {
    
}

void lRUCacheFree(LRUCache* obj) {
    free(obj.key);
    free(obj.value);
    free(obj);
}

/**
 * Your LRUCache struct will be instantiated and called as such:
 * LRUCache* obj = lRUCacheCreate(capacity);
 * int param_1 = lRUCacheGet(obj, key);
 
 * lRUCachePut(obj, key, value);
 
 * lRUCacheFree(obj);
*/