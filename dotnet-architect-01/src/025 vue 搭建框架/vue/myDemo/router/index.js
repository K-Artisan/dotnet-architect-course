const router = new VueRouter({
    routes: [
        { path: "/", redirect: jobList },
        { path: "/main", component: main, name: "main" },
        { path: "/jobList", component: jobList, name: "jobList" }
    ]
})