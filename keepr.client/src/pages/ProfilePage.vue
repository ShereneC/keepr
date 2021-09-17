<template>
  <div class="about text-center container">
    <!-- Top row for profile information -->
    <div class="row mt-2 mb-5">
      <div class="col-md-2">
        <img class="rounded" :src="profile.picture" alt="" />
      </div>
      <div class="col-md-10 d-flex flex-column align-items-start">
        <h1 class="text-break">
          {{ profile.name }}'s Profile
          <!-- Welcome {{ profile.name.split('@')[0] }} ! -->
        </h1>
        <h4>Vaults: {{ vaults.length }}</h4>
        <h4>Keeps:  {{ keeps.length }}</h4>
      </div>
    </div>
    <!-- Middle row for profile's vaults - TITLE -->
    <div class="row my-2">
      <div class="col-12 d-flex align-items-center">
        <h3 class="p-0">
          Vaults
        </h3>
        <div class="div">
          <h5 class="text-center"></h5>
        </div>
        <div class="div px-2" v-if="profile.id === account.id">
          <span class="fa fa-plus text-blue hoverable" title="Click to Add New" data-toggle="modal" data-target="#new-vault-modal"></span>
        </div>
      </div>
    </div>
    <!-- Middle row for profile's vaults - CARDS -->
    <div v-if="loading" class="col text-center">
      <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
    </div>
    <div class="row my-2" v-else>
      <div class="col d-flex justify-content-center">
        <div class="card-deck">
          <Vault v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
      </div>
    </div>
    <!-- does it matter where i put this modal call? -->
    <NewVaultModal />
    <!-- Bottom for keeps - TITLE only -->
    <div class="row my-2">
      <div class="col-12 d-flex align-items-center">
        <h3 class="p-0">
          Keeps
        </h3>
        <div class="div">
          <h5 class="text-center"></h5>
        </div>
        <div class="div px-2" v-if="profile.id === account.id">
          <span class="fa fa-plus text-blue hoverable" title="Create New Keep" data-toggle="modal" data-target="#new-keep-modal"></span>
        </div>
      </div>
    </div>
    <!-- Bottom row for profile's keeps - CARDS -->
    <div v-if="loading" class="col text-center">
      <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
    </div>
    <div class="row my-2" v-else>
      <div class="col-12">
        <div class="card-columns">
          <Keep v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
    <NewKeepModal />
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'

export default {
  setup() {
    const loading = ref(true)
    const route = useRoute()
    onMounted(async() => {
      try {
        // console.log('ProfilePage onMounted')
        await profilesService.getProfileById(route.params.id)
        // console.log(AppState.profile)
        await vaultsService.getVaultsByProfileId(route.params.id)
        // console.log(AppState.vaults)
        await keepsService.getKeepsByProfileId(route.params.id)
        // console.log(AppState.keeps)
        loading.value = false
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      loading,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.profile)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 300px;
}
.hoverable {
  cursor: pointer;
}
</style>
