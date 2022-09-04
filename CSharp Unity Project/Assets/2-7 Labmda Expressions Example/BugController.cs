using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UniRx;

/// <summary>
/// ���̃L�����N�^�[�𓮂������߂̃R���|�[�l���g
/// </summary>
public class BugController : MonoBehaviour, IPointerClickHandler
{
    // DOTween �Ɋւ���p�����[�^
    [Tooltip("�������̓����𐧌䂷��"), SerializeField] float _endValueX = 8f;
    [Tooltip("�c�����̓����𐧌䂷��"), SerializeField] float _endValueY = 1f;
    [Tooltip("�������̓����̃��[�v����"), SerializeField] float _timeX = 3f;
    [Tooltip("�c�����̓����̃��[�v����"), SerializeField] float _timeY = 1f;
    // UniRx �Ɋւ���p�����[�^/�v���p�e�B
    [Tooltip("�������C�t"), SerializeField] int _maxLife = 3;
    private readonly ReactiveProperty<int> _life = new IntReactiveProperty();

    void Start()
    {
        // DOTween ���g���ē��������
        transform.DOMoveX(_endValueX, _timeX).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalMoveY(_endValueY, _timeY).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        // UniRx �̏���
        _life.Value = _maxLife;
        _life.AddTo(this);
        // ���C�t�����������Ɏ��s���鏈����ݒ肷��
        this._life.Subscribe(_ => Debug.Log($"�����ꂽ! �c�胉�C�t: {_life}"));  // ����Ă΂�邱�Ƃ���������ꍇ�� Skip ���\�b�h���Ă�
    }

    void OnDestroy()
    {
        // DOTween ���~�߂�i�~�߂Ȃ��ƌx�����o��j
        DOTween.KillAll();
    }

    /// <summary>
    /// �I�u�W�F�N�g���N���b�N�����烉�C�t�����炵�A���C�t�� 0 �ɂȂ�����j������
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        this._life.Value--;

        if (this._life.Value < 1)
        {
            Destroy(this.gameObject);
        }
    }    
}
