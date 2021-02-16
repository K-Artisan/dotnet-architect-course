Vue.component('titleHeader', {
    template:
   `<div class="header">
        <div>
            <span class="title">招聘管理平台</span>
            <span class="login-user">用户：{{username}}</span>
        </div>
    </div>`,
    props:['username']
})