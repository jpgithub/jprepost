// DijsktraAlgo.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
using namespace std;
#include <limits.h>

// Number of vertices in the graph
#define V 9

// A utility function to find the vertex with minimum distance value, from
// the set of vertices not yet included in shortest path tree
int minDistance(int dist[], bool sptSet[])
{

	// Initialize min value
	int min = INT_MAX, min_index;

	for (int v = 0; v < V; v++)
	{
		// looking for false to indicate we have not visted yet.
		if (sptSet[v] == false && dist[v] <= min)
			min = dist[v], min_index = v;
	}

	return min_index;
}

// A utility function to print the constructed distance array
void printSolution(int dist[])
{
	cout << "Vertex \t Distance from Source" << endl;
	for (int i = 0; i < V; i++)
		cout << i << " \t\t" << dist[i] << endl;
}

// Function that implements Dijkstra's single source shortest path algorithm
// for a graph represented using adjacency matrix representation
void dijkstra(int graph[V][V], int src)
{
	int dist[V]; // The output array. dist[i] will hold the shortest
	// distance from src to i

	bool sptSet[V]; // sptSet[i] will be true if vertex i is included in shortest
	// path tree or shortest distance from src to i is finalized

	// Initialize all distances as INFINITE and stpSet[] as false
	for (int i = 0; i < V; i++)
	{
		dist[i] = INT_MAX, sptSet[i] = false;
	}
	// Distance of source vertex from itself is always 0
	dist[src] = 0;

	// Find shortest path for all vertices
	for (int count = 0; count < V - 1; count++) 
	{
		// Pick the minimum distance vertex from the set of vertices not
		// yet processed. u is always equal to src in the first iteration.
		int u = minDistance(dist, sptSet);

		// Mark the picked vertex as processed
		sptSet[u] = true;

		// Update dist value of the adjacent vertices of the picked vertex.
		for (int v = 0; v < V; v++)
		{
			// picked Vertex
			int src_distance_value = dist[u];

			// Update dist[v] only if is not in sptSet, there is an edge from
			// u to v, and total weight of path from src to v through u is
			// smaller than current value of dist[v]
			if (!sptSet[v] && graph[u][v] && src_distance_value != INT_MAX
				&& src_distance_value + graph[u][v] < dist[v])
			{
				dist[v] = src_distance_value + graph[u][v];
			}
		}
	}

	// print the constructed distance array
	printSolution(dist);
}

// driver program to test above function
int main()
{

	/* 
		Description First Dimension of the Array is Vertex, Second Dimenstion is Weights/Paths Length
		Index Association, each indexed element in the 1st dim will be represented as vertex.
		Each of the 1st dim indexed element will be have an assoicate 2nd dim (array) which will hold all the 
		weights associated with its neighbor vertex. A 0 will be use to indicate no association to this neighbor

		e.g take indexed element "0" it neighbors are 1 and 7 therefore the 2nd Dim (array) will be 
		{ 0, 4, 0, 0, 0, 0, 0, 8, 0 }

		index 1 is mark with weight 4 and index 7 is mark with weight 8

		Thus "0" only has two neighbors vertex.

	*/
	int graph[V][V] = { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
						{ 4, 0, 8, 0, 0, 0, 0, 11, 0 },
						{ 0, 8, 0, 7, 0, 4, 0, 0, 2 },
						{ 0, 0, 7, 0, 9, 14, 0, 0, 0 },
						{ 0, 0, 0, 9, 0, 10, 0, 0, 0 },
						{ 0, 0, 4, 14, 10, 0, 2, 0, 0 },
						{ 0, 0, 0, 0, 0, 2, 0, 1, 6 },
						{ 8, 11, 0, 0, 0, 0, 1, 0, 7 },
						{ 0, 0, 2, 0, 0, 0, 6, 7, 0 } };

	dijkstra(graph, 0);

	return 0;
}

// This code is contributed by shivanisinghss2110
