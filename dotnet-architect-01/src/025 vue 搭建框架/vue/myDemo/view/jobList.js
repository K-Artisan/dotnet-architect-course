jobList={
    template:`
        <div class="job-list">
            <table>
                <tr v-for="job in jobs">
                    <td class="job-info">
                        <p class="job-name">{{job.jobName}}</p>
                        <p class="job-pay">{{job.jobPay}}</p>
                        <p class="job-welfare">
                            <span v-for="welfare in getWelfare(job.welfare)">{{welfare}}</span>
                        </p>
                    </td>
                    <td class="job-position">
                        <p>{{job.companyAddress}}</p>
                        <p>{{job.city.city}}|{{job.education}}|{{job.workExperience}}</p>
                        <p>{{job.pulishTime}}</p>
                    </td> 
                </tr>
            </table>
        </div>`,
    data() {
        return {
            jobs:[]
        }
    },
    mounted() {
        this.getJobs();
    },
    methods: {
        getJobs(){
            axios.get("http://localhost:14511/Job")
                .then(res =>{
                    console.log("jobs=",res.data);
                    this.jobs=res.data;
                });
        },
        getWelfare(welfare){
            return welfare.split(",")
        }
    },
}