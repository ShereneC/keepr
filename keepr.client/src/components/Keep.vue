<template>
  <div class="keep card img-fluid rounded elevation-1 bg-light my-3 text-left selectable">
    <img class="card-img-top" :src="keep.img" alt="Card image" style="width:100%">
    <div class="card-img-overlay d-flex">
      <h5 class="text-light hoverable" title="Remove from vault" v-show="homepage == false" @click="removeKeep">
        X
      </h5>
      <h4 class="card-title text-light titleText mt-auto"
          data-toggle="modal"
          :data-target="'#keep-modal' + keep.id"
          title="See Keep Details"
      >
        {{ keep.name }}
      </h4>
      <router-link class="mt-auto ml-auto" :to="{name: 'ProfilePage', params: {id: keep.creatorId}}">
        <img :src="keep.creator.picture" alt="profile image" class="profile-pic mt-auto ml-auto">
      </router-link>
    </div>
  </div>
  <KeepModal :keep="keep" :vault="vault" />
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }

  },
  setup(props) {
    const route = useRoute()
    return {
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      homepage: computed(() => {
        if (route.path === '/' || route.path === '/profiles/613b964d03553023fe6ab5c4' || route.path === '/profiles/613b9e1845bd361e7d817142' || route.path === '/profiles/6133dba33605ba4cd98b9c35') {
          return true
        } else {
          return false
        }
      }),
      async setActiveKeep() {
        AppState.activeKeep = props.keep
      },
      async removeKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.removeKeepFromVault(props.keep.vaultKeepId)
            Pop.toast('Successfully Removed!', 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }

    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.titleText {
  text-shadow: -2px -2px 0 #000;
  // REVIEW  text-shadow: 2px 2px -2 #000; - not happy about this shadow, it will not outline all around the textand it won't let me put it on twice - only takes the first.
}

.profile-pic{
  height: 3rem;
  // width: 5vw;
  border-radius: 50%
  // object-fit: cover;
}
</style>
