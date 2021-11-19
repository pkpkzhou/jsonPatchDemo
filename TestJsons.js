
// endpoint: https://localhost:{port}/api/{controller}/{id}
// method: PATCH
// header: Content-Type: application/json-patch+json
// body: Copy and paste one of the JSON patch document samples from the JSON project folder.

// add

[
    {
        "op": "replace",
        "path": "/url",
        "value": "www.xcode.me"
    },
    {
        "op": "add",
        "path": "/posts/0",
        "value": {
            "blogId": 1,
            "title": "Third post",
            "content": "Test 3"
        }
    }
]

// remove

[
    {
        "op": "remove",
        "path": "/posts/0"
    },
    {
        "op": "remove",
        "path": "/posts/0/title"
    }
]

// replace

[
    {
        "op": "replace",
        "path": "/posts/0/content",
        "value": "Test 4"
    },
    {
        "op": "replace",
        "path": "/posts/1",
        "value": {
            "title": "Title 5",
            "content": "Test 5"
        }
    }
]

// move 

[
    {
        "op": "move",
        "from": "/posts/0/title",
        "path": "/posts/1/title"
    }
]

// copy

[
    {
        "op": "copy",
        "from": "/posts/0/title",
        "path": "/posts/1/content"
    }
]

// test

[
    {
        "op": "test",
        "path": "/url",
        "value": "www.xcode.me"
    },
    {
        "op": "test",
        "path": "/posts/0/title",
        "value": "Barry"
    }
]