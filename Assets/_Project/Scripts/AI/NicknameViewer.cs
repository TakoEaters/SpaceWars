using _Project.Scripts.Common;
using _Project.Scripts.General.Signals;
using Template.Scripts.General;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class NicknameViewer : MonoBehaviour
    {
        [SerializeField] private View _nickNameView;
        [SerializeField] private TextMeshPro _nickText;

        public string Nick { get; private set; }


        public void Initialize(Team team)
        {
            _nickText.color =  team == Team.Blue ? Color.blue : Color.red;
            _nickText.text = NicknamesGenerator.GetNickName(team);
            Nick = _nickText.text;
            _nickNameView.Enable();
        }
        public void EnableView()
        {
            _nickNameView.Enable();
        }

        public void DisableView()
        {
            _nickNameView.Disable();
        }
    }
}
